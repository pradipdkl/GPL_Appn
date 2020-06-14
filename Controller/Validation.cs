﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace GPL_Appn
{
    /// <summary>
    /// Simple validation class
    /// </summary>
    public class Validation
    {
        private Boolean isCmdValid = true;

        /// <summary>Gets or sets a value indicating whether this instance is command valid.</summary>
        /// <value>
        /// <c>true</c> if this instance is command valid; otherwise, <c>false</c>.</value>
        public bool IsCmdValid { get => isCmdValid; set => isCmdValid = value; }

        private Boolean isSyntaxValid = true;
        /// <summary>Gets or sets a value indicating whether this instance is Syntax valid.</summary>
        /// <value>
        /// <c>true</c> if this instance is syntax valid; otherwise, <c>false</c>.</value>
        public bool IsSyntaxValid { get => isSyntaxValid; set => isSyntaxValid = value; }
        
        private Boolean isParameterValid = true;

        /// <summary>Gets or sets a value indicating whether this instance is parameter valid.</summary>
        /// <value>
        /// <c>true</c> if this instance is parameter valid; otherwise, <c>false</c>.</value>
        public bool IsParameterValid { get => isParameterValid; set => isParameterValid = value; }

        private Boolean isSomethingInvalid = false;

        /// <summary>Gets or sets a value indicating whether this instance is command invalid.</summary>
        /// <value>
        /// <c>true</c> if this instance is command invalid; otherwise, <c>false</c>.</value>
        public bool IsSomethingInvalid { get => isSomethingInvalid; set => isSomethingInvalid = value; }

        /// <summary>
        /// lineNumber: indicates line number of the command in the multi-textline control.
        /// </summary>
        private int lineNumber = 0;

        /// <summary>
        /// doesCmdHasLoop: indicates whether command has "LOOP" keyword in the multi-textline control.
        /// </summary>
        private Boolean doesCmdHasLoop = false;

        /// <summary>
        /// doesCmdHasEndLoop: indicates whether command has "ENDLOOP" keyword in the multi-textline control.
        /// </summary>
        private Boolean doesCmdHasEndLoop = false;

        /// <summary>
        /// doesCmdHasIf: indicates whether command has "IF" keyword in the multi-textline control.
        /// </summary>
        private Boolean doesCmdHasIf = false;

        /// <summary>
        /// doesCmdHasEndif: indicates whether command has "ENDIF" keyword in the multi-textline control.
        /// </summary>
        private Boolean doesCmdHasEndif = false;

        /// <summary>
        /// doesCmdHasEndif: indicates whether command has "ENDIF" keyword in the multi-textline control.
        /// </summary>
        private int endIfLineNo = 0;
        private int loopLineNo;
        private int endLoopLineNo;
        private int ifLineNo;

        /// <summary>Gets or sets the line number.</summary>
        /// <value>The line number.</value>
        public int LineNumber { get => lineNumber; set => lineNumber = value; }

        /// <summary>Gets or sets a value indicating whether [does command has loop].</summary>
        /// <value>
        ///   <c>true</c> if [does command has loop]; otherwise, <c>false</c>.</value>
        public bool DoesCmdHasLoop { get => doesCmdHasLoop; set => doesCmdHasLoop = value; }

        /// <summary>Gets or sets a value indicating whether [does command has end loop].</summary>
        /// <value>
        ///   <c>true</c> if [does command has end loop]; otherwise, <c>false</c>.</value>
        public bool DoesCmdHasEndLoop { get => doesCmdHasEndLoop; set => doesCmdHasEndLoop = value; }

        /// <summary>Gets or sets a value indicating whether [does command has if].</summary>
        /// <value>
        ///   <c>true</c> if [does command has if]; otherwise, <c>false</c>.</value>
        public bool DoesCmdHasIf { get => doesCmdHasIf; set => doesCmdHasIf = value; }

        /// <summary>Gets or sets a value indicating whether [does command has endif].</summary>
        /// <value>
        ///   <c>true</c> if [does command has endif]; otherwise, <c>false</c>.</value>
        public bool DoesCmdHasEndif { get => doesCmdHasEndif; set => doesCmdHasEndif = value; }

        /// <summary>Gets or sets the loop line no.</summary>
        /// <value>The loop line no.</value>
        public int LoopLineNo { get => loopLineNo; set => loopLineNo = value; }

        /// <summary>Gets or sets the end loop line no.</summary>
        /// <value>The end loop line no.</value>
        public int EndLoopLineNo { get => endLoopLineNo; set => endLoopLineNo = value; }

        /// <summary>Gets or sets if line no.</summary>
        /// <value>If line no.</value>
        public int IfLineNo { get => ifLineNo; set => ifLineNo = value; }

        /// <summary>Gets or sets the end if line no.</summary>
        /// <value>The end if line no.</value>
        public int EndIfLineNo { get => endIfLineNo; set => endIfLineNo = value; }

        TextBox textBoxCmd;
        /// <summary>
        /// Check the command validations.
        /// </summary>
        /// <param name="textBoxCmd"></param>
        public Validation(TextBox textBoxCmd)
        {
            this.textBoxCmd = textBoxCmd;
            int numberOfCmdLines = textBoxCmd.Lines.Length;
            if (numberOfCmdLines == 0) 
            {
                IsCmdValid = false;
            }
            else
            {
                for (int i = 0; i < numberOfCmdLines; i++)
                {
                    String singleLineCmd = textBoxCmd.Lines[i];
                    singleLineCmd = singleLineCmd.Trim();
                    if (!singleLineCmd.Equals(""))
                    {
                        CheckCmdLineValidation(singleLineCmd);
                        LineNumber = (i + 1);
                        if (!IsCmdValid)
                        {
                            if (!IsParameterValid)
                            {
                                MessageBox.Show("Paramter error at Line : " + LineNumber);
                            }
                            else if (!IsSyntaxValid)
                            {
                                MessageBox.Show("Command Not Found. Error at Line : " + LineNumber);
                            }
                            else
                            {
                                MessageBox.Show("Command error at Line: " + LineNumber);
                            }
                            IsSomethingInvalid = true;
                        }
                    }
                }
                CheckCmdLoopAndIfValidation();
                if (!IsCmdValid)
                {
                    IsSomethingInvalid = true;
                }
            }
        }
        /// <summary>
        /// Check commands for any error such as syntax, parameter or validity.
        /// </summary>
        /// <param name="cmd">String</param>
        public void CheckCmdLineValidation(string cmd)
        {
            String[] syntaxs = { "drawto", "moveto", "clear","reset", "run","loop","endif","endloop","if" };
            String[] shapes = { "circle", "rectangle", "triangle" };
            String[] variables = { "radius", "width", "height", "counter", "hypotenuse"};
           
            cmd = Regex.Replace(cmd, @"\s+", " ");
            string[] commandsAfterSpliting = cmd.Split(' ');
            for (int i = 0; i < commandsAfterSpliting.Length; i++)
            {
                commandsAfterSpliting[i] = commandsAfterSpliting[i].Trim();
            }
            String firstWord = commandsAfterSpliting[0].ToLower();
            Boolean firstWordIsSyntax = syntaxs.Contains(firstWord);
            Boolean firstWordIsShape = shapes.Contains(firstWord);
            Boolean firstWordIsVariable = variables.Contains(firstWord);

            if (firstWordIsSyntax)
            {
                if (firstWord.Equals("drawto") || firstWord.Equals("moveto"))
                {
                    String args = cmd.Substring(6, (cmd.Length - 6));
                    String[] parms = args.Split(',');

                    if (parms.Length == 2)
                    {
                        for (int i = 0; i < parms.Length; i++)
                        {
                            if (!parms[i].Trim().All(char.IsDigit))
                            {
                                IsCmdValid = false;
                            }
                        }
                    }
                    else
                    {
                        IsCmdValid = false;
                    }
                }
                else if (firstWord.Equals("clear") || firstWord.Equals("reset"))
                {
                    IsCmdValid = true;
                }
                else if (firstWord.Equals("loop"))
                {
                    if (commandsAfterSpliting.Length == 2)
                    {
                        if (!commandsAfterSpliting[1].Trim().All(char.IsDigit))
                        {
                            IsCmdValid = false;
                        }
                    }
                    else
                    {
                        IsCmdValid = false;
                    }
                }
                else if (firstWord.Equals("endloop"))
                {
                    if (commandsAfterSpliting.Length == 1)
                    {
                        if (!commandsAfterSpliting[0].Equals("endloop"))
                        {
                            IsCmdValid = false;
                        }
                    }
                    else
                    {
                        IsCmdValid = false;
                    }
                }//endif
                else if (firstWord.Equals("if"))//if radius = x then
                {
                    if (commandsAfterSpliting.Length == 5)
                    {
                        if (variables.Contains(commandsAfterSpliting[1].ToLower()))
                        {
                            if (commandsAfterSpliting[2].Equals("="))
                            {
                                if (commandsAfterSpliting[3].Trim().All(char.IsDigit))
                                {
                                    if (!commandsAfterSpliting[4].ToLower().Equals("then"))
                                    {
                                        IsCmdValid = false;
                                    }
                                }
                                else { IsCmdValid = false; }
                            }
                            else { IsCmdValid = false; }
                        }
                        else { IsCmdValid = false; }

                    }
                    else { IsCmdValid = false; }

                }
                else if (firstWord.Equals("endif"))
                {
                    if (commandsAfterSpliting.Length != 1)
                    {
                        IsCmdValid = false;
                    }
                }
            }
            else if (firstWordIsShape)
            {
                if (firstWord.ToLower().Equals("circle"))
                {
                    if (commandsAfterSpliting.Length == 2)
                    {
                        if (commandsAfterSpliting[1].Trim().All(char.IsDigit))
                        { 
                        }
                        else if (commandsAfterSpliting[1].Trim().All(char.IsLetter))
                        {
                            if (variables.Contains(commandsAfterSpliting[1].ToLower()))
                            {
                                checkIfVariableDefined(commandsAfterSpliting[1]);
                            }
                            else 
                            { 
                                IsCmdValid = false; 
                                IsParameterValid = false;
                            }
                        }
                        else 
                        { 
                            IsCmdValid = false; 
                            IsParameterValid = false;
                        }
                    }
                    else 
                    { 
                        IsCmdValid = false; 
                        IsParameterValid = false; 
                    }
                }
                else if (firstWord.ToLower().Equals("rectangle"))
                {
                    String args = cmd.Substring(9, (cmd.Length - 9));
                    String[] parms = args.Split(',');

                    if (parms.Length == 2)
                    {
                        for (int i = 0; i < parms.Length; i++)
                        {
                            parms[i] = parms[i].Trim();
                            if (parms[i].All(char.IsDigit)) 
                            {
                            }
                            else if (parms[i].All(char.IsLetter))
                            {
                                if (variables.Contains(parms[i].ToLower())) 
                                {
                                    checkIfVariableDefined(parms[i]);
                                }
                                else 
                                { 
                                    IsCmdValid = false;
                                    IsParameterValid = false;
                                }
                            }
                            else
                            { 
                                IsCmdValid = false; 
                                IsParameterValid = false; 
                            }
                        }
                    }
                    else 
                    {
                        IsCmdValid = false;
                        IsParameterValid = false;
                    }
                }
                else if (firstWord.ToLower().Equals("triangle"))
                {
                    String args = cmd.Substring(8, (cmd.Length - 8));
                    String[] parms = args.Split(',');

                    if (parms.Length == 3)
                    {
                        for (int i = 0; i < parms.Length; i++)
                        {
                            parms[i] = parms[i].Trim();
                            if (parms[i].All(char.IsDigit))
                            {
                            }
                            else if (parms[i].All(char.IsLetter))
                            {
                                if (variables.Contains(parms[i].ToLower()))
                                {
                                    checkIfVariableDefined(parms[i]);
                                }
                                else
                                {
                                    IsCmdValid = false;
                                    IsParameterValid = false;
                                }
                            }
                            else
                            { 
                                IsCmdValid = false; 
                                IsParameterValid = false; 
                            }
                        }
                    }
                    else 
                    {
                        IsCmdValid = false; 
                        IsParameterValid = false; 
                    }
                }
            }
            else if (firstWordIsVariable)
            {
                if (commandsAfterSpliting.Length == 3)
                {
                    if (commandsAfterSpliting[1].Equals("="))
                    {
                        if (!commandsAfterSpliting[2].Trim().All(char.IsDigit)) 
                        {
                            IsCmdValid = false; 
                            IsParameterValid = false;
                        }
                    }
                    else
                    {
                        IsCmdValid = false;
                    }
                }
                else
                {
                    IsCmdValid = false; }
            }
            else 
            {
                IsCmdValid = false;
                isSyntaxValid = false;
            }
            if (!IsCmdValid) 
            {
                IsSomethingInvalid = true;
            }
        }
        /// <summary>
        /// Check the loop and if command validation.
        /// </summary>
        public void CheckCmdLoopAndIfValidation()
        {
            int numberOfLines = textBoxCmd.Lines.Length;
            for (int i = 0; i < numberOfLines; i++)
            {
                String singleLineCmd = textBoxCmd.Lines[i];
                singleLineCmd = singleLineCmd.Trim();
                if (!singleLineCmd.Equals(""))
                {
                    doesCmdHasLoop = Regex.IsMatch(singleLineCmd.ToLower(), @"\bloop\b");
                    if (doesCmdHasLoop)
                    {
                        loopLineNo = (i + 1);
                    }
                    doesCmdHasEndLoop = singleLineCmd.ToLower().Contains("endloop");
                    if (doesCmdHasEndLoop)
                    {
                        endLoopLineNo = (i + 1);
                    }
                    doesCmdHasIf = Regex.IsMatch(singleLineCmd.ToLower(), @"\bif\b");
                    if (doesCmdHasIf)
                    {
                        ifLineNo = (i + 1);
                    }
                    doesCmdHasEndif = singleLineCmd.ToLower().Contains("endif");
                    if (doesCmdHasEndif)
                    {
                        endIfLineNo = (i + 1);
                    }
                }
            }
            if (loopLineNo > 0)
            {
                doesCmdHasLoop = true;
            }
            if (endLoopLineNo > 0)
            {
                doesCmdHasLoop = true;
            }
            if (ifLineNo > 0)
            {
                doesCmdHasIf = true;
            }
            if (endIfLineNo > 0)
            {
                doesCmdHasEndif = true;
            }
            if (doesCmdHasLoop)
            {
                if (doesCmdHasEndLoop)
                {
                    if (loopLineNo < endLoopLineNo)
                    {

                    }
                    else
                    {
                        IsCmdValid = false;
                        MessageBox.Show("'ENDLOOP' must be after loop start");
                    }
                }
                else
                {
                    IsCmdValid = false;
                    MessageBox.Show("Loop Not Ended with 'ENDLOOP'");
                }
            }
            if (doesCmdHasIf)
            {
                if (doesCmdHasIf)
                {
                    if (ifLineNo < endIfLineNo)
                    {

                    }
                    else
                    {
                        IsCmdValid = false;
                        MessageBox.Show("'ENDIF' must be after IF");
                    }
                }
                else
                {
                    IsCmdValid = false;
                    MessageBox.Show("IF Not Ended with 'ENDIF'");
                }
            }
        }
        /// <summary>
        /// check the variable whether defined or not.
        /// </summary>
        /// <param name="variable"></param>
        public void checkIfVariableDefined(string variable)
        {
            Boolean isvariablefound = false;
            if (textBoxCmd.Lines.Length > 1)
            {
                if (lineNumber > 0)
                {
                    for (int i = 0; i < lineNumber; i++)
                    {
                        String oneLineCommand = textBoxCmd.Lines[i];
                        oneLineCommand = oneLineCommand.Trim();
                        if (!oneLineCommand.Equals(""))
                        {
                            Boolean isVariableDefined = oneLineCommand.ToLower().Contains(variable.ToLower());
                            if (isVariableDefined)
                            {
                                isvariablefound = true;
                            }
                        }
                    }
                    if (!isvariablefound)
                    {
                        MessageBox.Show("Variable is not defined");
                        IsCmdValid = false;
                    }
                }
                else
                {
                    MessageBox.Show("Variable is not defined");
                    IsCmdValid = false;
                }
            }
            else
            {
                MessageBox.Show("Variable is not defined");
                IsCmdValid = false;
            }
        }
    }
}

