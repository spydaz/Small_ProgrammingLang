using System;
using System.ComponentModel;
using System.Diagnostics;

namespace SDK.My
{
    internal static partial class MyProject
    {
        internal partial class MyForms
        {
            [EditorBrowsable(EditorBrowsableState.Never)]
            public EMU_MachineUI m_EMU_MachineUI;

            public EMU_MachineUI EMU_MachineUI
            {
                [DebuggerHidden]
                get
                {
                    m_EMU_MachineUI = Create__Instance__(m_EMU_MachineUI);
                    return m_EMU_MachineUI;
                }

                [DebuggerHidden]
                set
                {
                    if (ReferenceEquals(value, m_EMU_MachineUI))
                        return;
                    if (value is object)
                        throw new ArgumentException("Property can only be set to Nothing");
                    Dispose__Instance__(ref m_EMU_MachineUI);
                }
            }

            [EditorBrowsable(EditorBrowsableState.Never)]
            public FormDisplayConsole m_FormDisplayConsole;

            public FormDisplayConsole FormDisplayConsole
            {
                [DebuggerHidden]
                get
                {
                    m_FormDisplayConsole = Create__Instance__(m_FormDisplayConsole);
                    return m_FormDisplayConsole;
                }

                [DebuggerHidden]
                set
                {
                    if (ReferenceEquals(value, m_FormDisplayConsole))
                        return;
                    if (value is object)
                        throw new ArgumentException("Property can only be set to Nothing");
                    Dispose__Instance__(ref m_FormDisplayConsole);
                }
            }

            [EditorBrowsable(EditorBrowsableState.Never)]
            public FormDisplayGraphics m_FormDisplayGraphics;

            public FormDisplayGraphics FormDisplayGraphics
            {
                [DebuggerHidden]
                get
                {
                    m_FormDisplayGraphics = Create__Instance__(m_FormDisplayGraphics);
                    return m_FormDisplayGraphics;
                }

                [DebuggerHidden]
                set
                {
                    if (ReferenceEquals(value, m_FormDisplayGraphics))
                        return;
                    if (value is object)
                        throw new ArgumentException("Property can only be set to Nothing");
                    Dispose__Instance__(ref m_FormDisplayGraphics);
                }
            }

            [EditorBrowsable(EditorBrowsableState.Never)]
            public SAL_ZX21_SPL_BASIC_REPL m_SAL_ZX21_SPL_BASIC_REPL;

            public SAL_ZX21_SPL_BASIC_REPL SAL_ZX21_SPL_BASIC_REPL
            {
                [DebuggerHidden]
                get
                {
                    m_SAL_ZX21_SPL_BASIC_REPL = Create__Instance__(m_SAL_ZX21_SPL_BASIC_REPL);
                    return m_SAL_ZX21_SPL_BASIC_REPL;
                }

                [DebuggerHidden]
                set
                {
                    if (ReferenceEquals(value, m_SAL_ZX21_SPL_BASIC_REPL))
                        return;
                    if (value is object)
                        throw new ArgumentException("Property can only be set to Nothing");
                    Dispose__Instance__(ref m_SAL_ZX21_SPL_BASIC_REPL);
                }
            }

            [EditorBrowsable(EditorBrowsableState.Never)]
            public VM_MachineUI m_VM_MachineUI;

            public VM_MachineUI VM_MachineUI
            {
                [DebuggerHidden]
                get
                {
                    m_VM_MachineUI = Create__Instance__(m_VM_MachineUI);
                    return m_VM_MachineUI;
                }

                [DebuggerHidden]
                set
                {
                    if (ReferenceEquals(value, m_VM_MachineUI))
                        return;
                    if (value is object)
                        throw new ArgumentException("Property can only be set to Nothing");
                    Dispose__Instance__(ref m_VM_MachineUI);
                }
            }
        }
    }
}