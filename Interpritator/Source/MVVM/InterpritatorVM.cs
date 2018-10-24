using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows;
using Interpritator.Source.Convertrs;
using Interpritator.Source.Interpritator;
using Interpritator.Source.Interpritator.Command;
using Interpritator.Source.MVVM.Models;
using Interpritator.Source.UserInterfaceUtilities;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Win32;

namespace Interpritator.Source.MVVM
{
    internal class InterpritatorVM : BindableBase
    {

        private FileInfo _currentFilePath;

        private string _resultOutput;
        private string _errorOutput;
        private string _commandInput;

        private List<string> _commandsList;
        private int _currentCommand; 


        public string CurrentFileName
        {
            get => _currentFilePath?.Name ?? "file not open";
            set => _currentFilePath = new FileInfo(value);
        }

        public ObservableCollection<BreakPoint> BreakPointsList { get; set; }


        public string CommandInput
        {
            get => _commandInput;
            set
            {
                _commandInput = value;
                Update();

                OnPropertyChanged("CommandInput");
            }
        }

        public string ResultOutput
        {
            get => _resultOutput;
            set
            {
                _resultOutput = value;
                OnPropertyChanged("ResultOutput");
            }
        }

        public string ErrorOutput
        {
            get => _errorOutput;
            set
            {
                _errorOutput = value;
                OnPropertyChanged("ErrorOutput");
            }
        }

        public bool IsDebugMod { get; private set; } = false;

        public bool IsSimpleMode => !IsDebugMod;

        public InterpritatorVM()
        {
            _currentCommand = -1;
            _currentFilePath = null;
            BreakPointsList = new ObservableCollection<BreakPoint>();

            SaveFileCommand    = new DelegateCommand(Save);
            SaveFileAsCommand    = new DelegateCommand(SaveAs);

            ExitCommand = new DelegateCommand(Exit);

            StartCommand = new DelegateCommand(Start);
            StartDebugCommand = new DelegateCommand(StartDebug);
            StepOutCommand = new DelegateCommand(StepToNextBp);
            NextStepCommand = new DelegateCommand(Step);
        }


        #region Commands
        public DelegateCommand OpenFileCommand {get; set;}
        public DelegateCommand SaveFileCommand {get; set;}
        public DelegateCommand SaveFileAsCommand {get; set;}
        public DelegateCommand ExitCommand { get; set; }

        public DelegateCommand StartCommand { get; set; }
        public DelegateCommand StartDebugCommand { get; set; }
        public DelegateCommand NextStepCommand { get; set; }
        public DelegateCommand StepOutCommand { get; set; }


        #endregion


        #region Menu Func

        private void SaveAs()
        {
            var isGoodDialogResult = SaveFileDialog();
            if (isGoodDialogResult)
            {
                MainMenuFunc.SaveFile(_currentFilePath.FullName, _commandsList);
            }
        }

        private void Save()
        {
            if (_currentFilePath == null)
            {
                var isGoodDialogResult = SaveFileDialog();
                if (isGoodDialogResult)
                {
                    MainMenuFunc.SaveFile(_currentFilePath.FullName, _commandsList);
                }
            }
            else
            {
                MainMenuFunc.SaveFile(_currentFilePath.FullName, _commandsList);
            }
        }


        private void Exit()
        {
            Application.Current.Shutdown();
        }

        #region Run Menu
        private void Start()
        {
            var patch = SaveBinFileDialog();
            if (patch != null)
            {
                try
                {
                    Compiler.SaveToBinFile(patch, _commandsList);
                    var interpritatorResult = NumberCommandInterpritator.StartProgram(patch);

                    ResultOutput = interpritatorResult.Key;
                    ErrorOutput = interpritatorResult.Value;
                }
                catch (CompilerException ce)
                {
                    ErrorOutput = "#Error: " + ce.Message + "-->" + ce.WrongCommand + "Command № [" + ce.CommandNumber + "]\n";
                }

            }
        }

        private void StartDebug()
        {
            _resultOutput = "";
            _errorOutput = "";
            IsDebugMod = true;
            OnPropertyChanged("IsDebugMod");
            OnPropertyChanged("IsSimpleMode");

            _currentCommand = 0;
            StepToNextBp(); 
           
        }
        #endregion

        #endregion


        #region Dialogs

        private bool OpenFileDialog()
        {
            var openFileDialog = new OpenFileDialog
            {
                AddExtension = true,
                DefaultExt = "inw",
                Filter = "Simple interpritator text file|*.inw"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                CurrentFileName = openFileDialog.FileName;
                return true;
            }
            return false;
        }

        private bool SaveFileDialog()
        {

            var saveFileDialog = new SaveFileDialog
            {
                AddExtension = true,
                DefaultExt = "inw",
                Filter = "Simple interpritator text file|*.inw"
            };

            if (saveFileDialog.ShowDialog() == true)
            {

                CurrentFileName = saveFileDialog.FileName;
                return true;
            }
            return false;
        }

        private string OpenBinFileDialog()
        {
            string newFilePatch = null;
            var openFileDialog = new OpenFileDialog
            {
                AddExtension = true,
                DefaultExt = "inwoi",
                Filter = "Command interpritator file|*.inwoi"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                newFilePatch = openFileDialog.FileName;
            }
            return newFilePatch;
        }

        private string SaveBinFileDialog()
        {
            string newFilePatch = null;
            var saveFileDialog = new SaveFileDialog
            {
                AddExtension = true,
                DefaultExt = "inwoi",
                Filter = "Command interpritator file|*.inwoi"
            };

            if (saveFileDialog.ShowDialog() == true)
            {

                newFilePatch = saveFileDialog.FileName;
            }
            return newFilePatch;
        }

        #endregion


        private void Update()
        {
            _commandsList = ArrToOneStringConverter.ConvertBack(_commandInput).ToList();
            GenerateBreakPoints();
        }

        private void GenerateBreakPoints()
        {
            var linesCount = GetCharCount('\n') + 1;
            var isSimilarSizes = BreakPointsList.Count == linesCount;
            if(isSimilarSizes) return;

            var differences = BreakPointsList.Count - linesCount;

            if (differences > 0 && BreakPointsList.Count != 0)
            {
                for (var i = 0; i < differences; i++)
                {
                    BreakPointsList.RemoveAt(BreakPointsList.Count - 1);
                }
            }
            else 
            {
                for (var i = 0; i < -differences; i++)
                {
                    BreakPointsList.Add(new BreakPoint());
                }
            }

            OnPropertyChanged("BreakPointsList");
        }

        private int GetCharCount(char findingChar)
        {
            var result = 0;
            foreach (var character in CommandInput)
            {
                if (character == findingChar) result++;
            }
            return result;
        }

        private void StepToNextBp()
        {
            try
            {
                if (BreakPointsList.Any())
                {
                    while (!BreakPointsList[_currentCommand].IsEnabled)
                    {
                        var strCommand = CommandInput.Split('\n')[_currentCommand];

                        var bitCommand = Compiler.CommandToBit(strCommand.Trim());

                        var interpritatorResult = NumberCommandInterpritator.RunCommand(bitCommand);

                        ResultOutput += interpritatorResult.Key;
                        ErrorOutput += interpritatorResult.Value;
                        _currentCommand++;
                        if (_currentCommand >= BreakPointsList.Count)
                        {
                            EndDebug();
                            break;

                        }
                    }
                }

            }
            catch (CompilerException ce)
            {
                ErrorOutput = "#Error: " + ce.Message + "-->" + ce.WrongCommand + "Command № [" + ce.CommandNumber + "]\n";
            }
        }

        private void Step()
        {
            try
            {
                if (BreakPointsList.Any())
                {
                    var strCommand = CommandInput.Split('\n')[_currentCommand];

                    var bitCommand = Compiler.CommandToBit(strCommand.Trim());

                    var interpritatorResult = NumberCommandInterpritator.RunCommand(bitCommand);

                    ResultOutput += interpritatorResult.Key;
                    ErrorOutput += interpritatorResult.Value;
                    _currentCommand++;
                    if (_currentCommand >= BreakPointsList.Count)
                    {
                        EndDebug();
                    }
                }

            }
            catch (CompilerException ce)
            {
                ErrorOutput = "#Error: " + ce.Message + "-->" + ce.WrongCommand + "Command № [" + ce.CommandNumber + "]\n";
            }
        }

        private void EndDebug()
        {
            IsDebugMod = false;
            _currentCommand = 0;

            OnPropertyChanged("IsDebugMod");
            OnPropertyChanged("IsSimpleMode");

        }
    }
}
