' MIT License
' 
' Copyright (c) 2016 Yve Verstrepen
' 
' Permission is hereby granted, free of charge, to any person obtaining a copy
' of this software and associated documentation files (the "Software"), to deal
' in the Software without restriction, including without limitation the rights
' to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
' copies of the Software, and to permit persons to whom the Software is
' furnished to do so, subject to the following conditions:
' 
' The above copyright notice and this permission notice shall be included in all
' copies or substantial portions of the Software.
' 
' THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
' IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
' FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
' AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
' LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
' OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
' SOFTWARE.

Imports System
Imports System.IO
Imports EMU6502

Namespace Simple64
    Friend Class Program
        Public Shared Sub Main(ByVal args As String())
            Call (New Program()).Run()
        End Sub

        Private _cpu As MOS6502
        Private _ram As RAM64K

        Private Sub Run()
            _ram = New RAM64K()
            _cpu = New MOS6502(_ram)
            Console.Title = "6502"
            Console.BackgroundColor = CType(6, ConsoleColor)
            Console.ForegroundColor = CType(14, ConsoleColor)
            Console.SetWindowSize(40, 26)
            Console.SetBufferSize(40, 26)
            Console.CursorSize = 100



            'Temp
            'TestB();
            'return;

            '_ram.Write(0x0000, 0x2F);
            '_ram.Write(0x0001, 0x37);

            '_ram.Write(0xD018, 21);

            Using file As FileStream = New FileStream("BASIC.ROM", FileMode.Open, FileAccess.Read)
                _ram.Load(file, &HA000, 8192)
            End Using

            Using file As FileStream = New FileStream("CHAR.ROM", FileMode.Open, FileAccess.Read)
                _ram.Load(file, &HD000, 4096)
            End Using

            Using file As FileStream = New FileStream("KERNAL.ROM", FileMode.Open, FileAccess.Read)
                _ram.Load(file, &HE000, 8192)
            End Using

            Dim screenbuffer = New Byte(999) {}
            Dim raster As Byte = 0

            While True
                _cpu.Process()
                'Console.WriteLine(cpu.PC.ToString("X4"));
                _ram.Write(&HD012, raster)
                raster += 1 ' if (raster == 312) raster = 0;
                'Console.Clear();
                If _cpu.Cycles Mod 10000 = 0 Then
                    If Console.KeyAvailable Then
                        Dim key = Console.ReadKey(True)
                        _ram.Write16(&HDC00, key.Key)
                    Else
                        _ram.Write16(&HDC00, &H0)
                    End If

                    'Console.Title = _cpu.PC.ToString("X4");
                    Console.Title = _ram.Read16(&HDC00).ToString()

                    ' Address where the C64 character screen buffer is located
                    Dim screenAddress As UShort = _ram(&H288) << 8

                    For i As UShort = 0 To 1000 - 1
                        Dim data = _ram.Read(i + screenAddress)
                        If data < &H20 Then data += &H40
                        'data &= 0x7F; // Reverse
                        If data <> screenbuffer(i) Then
                            Console.CursorVisible = False

                            If (data And &H80) <> 0 Then
                                Console.BackgroundColor = CType(14, ConsoleColor)
                                Console.ForegroundColor = CType(6, ConsoleColor)
                            End If

                            Console.SetCursorPosition(i Mod 40, i / 40)
                            Console.Write(Microsoft.VisualBasic.ChrW(data))

                            If (data And &H80) <> 0 Then
                                Console.BackgroundColor = CType(6, ConsoleColor)
                                Console.ForegroundColor = CType(14, ConsoleColor)
                            End If
                        End If

                        screenbuffer(i) = data
                    Next

                    If _ram(&HCC) = 0 Then ' Draw cursor when visible
                        Dim x As Integer = _ram(&HCA)
                        Dim y As Integer = _ram(&HC9)
                        If Console.CursorLeft <> x OrElse Console.CursorTop <> y Then Console.SetCursorPosition(x, y)
                        If Not Console.CursorVisible Then Console.CursorVisible = True
                    End If
                End If
                'System.Threading.Thread.Sleep(50);

                ' 
                ' cpu.Op();
                ' Console.WriteLine("test00: " + ram.Read(0x022A).ToString("X2"));
                ' Console.WriteLine("test01: " + ram.Read(0xA9).ToString("X2"));
                ' Console.WriteLine("test02: " + ram.Read(0x71).ToString("X2"));
                ' System.Threading.Thread.Sleep(50);
                ' if (cpu.PC == 0x45C0)
                ' {
                ' 
                ' Console.WriteLine("FINAL: " + ram.Read(0x0210).ToString("X2"));
                ' System.Threading.Thread.Sleep(50);
                ' }
                ' 
            End While
        End Sub
    End Class





End Namespace

