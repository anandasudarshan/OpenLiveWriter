// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for details.

using System;
using System.Runtime.InteropServices;

namespace OpenLiveWriter.Interop.Com
{
    /*

    /// <summary>
    /// Generic COM/OLE command dispatching interface
    /// </summary>
    [ComImport]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [Guid("b722bccb-4e68-101b-a2bc-00aa00404770")]
    public interface IOleCommandTargetTest
    {
        /// <summary>
        /// Queries the object for the status of one or more commands generated by user
        /// interface events. Note that because we couldn't get COM interop to correctly
        /// marshall the array of OLECMDs, our declaration of this interface supports
        /// only querying for status on a single-command. This is not a problem as almost
        /// all instances where you would implement IOleCommandTarget are for a single
        /// command. Implementations should ASSERT that cCmds is 1
        /// </summary>
        /// <param name="pguidCmdGroup"> Unique identifier of the command group; can be NULL to
        /// specify the standard group. All the commands that are passed in the prgCmds
        /// array must belong to the group specified by pguidCmdGroup</param>
        /// <param name="cCmds">The number of commands in the prgCmds array. For this
        /// interface declaration (which doesn't support arrays of OLECMD) this value
        /// MUST always be 1.</param>
        /// <param name="prgCmds">Reference to the command that is being queried for
        /// its status -- this parameter should be filled in with the appropriate
        /// values.</param>
        /// <param name="pCmdText">Pointer to an OLECMDTEXT structure in which to return
        /// name and/or status information of a single command. Can be NULL to indicate
        /// that the caller does not need this information. Note that because of
        /// marshalling issues w/ the OLECMDTEXT structure (can't figure out how to
        /// marshall it correctly) we required that this parameter be NULL (implementations
        /// should Assert on this). Note that IE currently passes NULL for this parameter
        /// for custom toolbar button implementations.</param>
        void QueryStatus(
            IntPtr pguidCmdGroup,
            uint cCmds,
            IntPtr prgCmds,
            IntPtr pCmdText);

        /// <summary>
        /// Executes a specified command or displays help for a command
        /// </summary>
        /// <param name="pguidCmdGroup">Pointer to unique identifier of the command group; can be
        /// NULL to specify the standard group</param>
        /// <param name="nCmdID">The command to be executed. This command must belong to the
        /// group specified with pguidCmdGroup</param>
        /// <param name="nCmdexecopt">Values taken from the OLECMDEXECOPT enumeration, which
        /// describe how the object should execute the command</param>
        /// <param name="pvaIn">Pointer to a VARIANTARG structure containing input arguments.
        /// Can be NULL</param>
        /// <param name="pvaOut">Pointer to a VARIANTARG structure to receive command output.
        /// Can be NULL.</param>
        void Exec(
            IntPtr pguidCmdGroup,
            uint nCmdID,
            OLECMDEXECOPT nCmdexecopt,
            IntPtr pvaIn,
            IntPtr pvaOut ) ;
    }
    */

    /// <summary>
    /// Generic COM/OLE command dispatching interface
    /// </summary>
    [ComImport]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [Guid("b722bccb-4e68-101b-a2bc-00aa00404770")]
    public interface IOleCommandTarget
    {
        /// <summary>
        /// Queries the object for the status of one or more commands generated by user
        /// interface events.
        /// </summary>
        /// <param name="pguidCmdGroup"> Unique identifier of the command group; can be NULL to
        /// specify the standard group. All the commands that are passed in the prgCmds
        /// array must belong to the group specified by pguidCmdGroup</param>
        /// <param name="cCmds">The number of commands in the prgCmds array.</param>
        /// <param name="prgCmds">Reference to the command that is being queried for
        /// its status -- this parameter should be filled in with the appropriate
        /// values.</param>
        /// <param name="pCmdText">Pointer to an OLECMDTEXT structure in which to return
        /// name and/or status information of a single command. Can be NULL to indicate
        /// that the caller does not need this information. Note that because of
        /// marshalling issues w/ the OLECMDTEXT structure (can't figure out how to
        /// marshall it correctly) we required that this parameter be NULL (implementations
        /// should Assert on this). Note that IE currently passes NULL for this parameter
        /// for custom toolbar button implementations.</param>
        [PreserveSig]
        int QueryStatus(
            [In] ref Guid pguidCmdGroup,
            [In] uint cCmds,
            [In, Out, MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 1)] OLECMD[] prgCmds,
            [In, Out] IntPtr pCmdText);

        /// <summary>
        /// Executes a specified command or displays help for a command
        /// </summary>
        /// <param name="pguidCmdGroup">Pointer to unique identifier of the command group; can be
        /// NULL to specify the standard group</param>
        /// <param name="nCmdID">The command to be executed. This command must belong to the
        /// group specified with pguidCmdGroup</param>
        /// <param name="nCmdexecopt">Values taken from the OLECMDEXECOPT enumeration, which
        /// describe how the object should execute the command</param>
        /// <param name="pvaIn">Pointer to a VARIANTARG structure containing input arguments.
        /// Can be NULL</param>
        /// <param name="pvaOut">Pointer to a VARIANTARG structure to receive command output.
        /// Can be NULL.</param>
        [PreserveSig]
        int Exec(
            [In] ref Guid pguidCmdGroup,
            [In] uint nCmdID,
            [In] OLECMDEXECOPT nCmdexecopt,
            [In] IntPtr pvaIn,
            [In, Out] IntPtr pvaOut);
    }

    /// <summary>
    /// Generic COM/OLE command dispatching interface. This version of the declaration
    /// declares the two optional Exec parameters as ref object to allow for passing
    /// parameters to the Exec method. The reason we need to do this is that these
    /// parameters are defined as VARIANTARG* however the value passed can be NULL. If
    /// we declare them as object and the caller passes NULL then the runtime blows up.
    /// We therefore need two separate declarations for the interface depending upon
    /// its use.
    /// </summary>
    [ComImport]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [Guid("b722bccb-4e68-101b-a2bc-00aa00404770")]
    public interface IOleCommandTargetWithExecParams
    {
        /// <summary>
        /// Queries the object for the status of one or more commands generated by user
        /// interface events. Note that because we couldn't get COM interop to correctly
        /// marshall the array of OLECMDs, our declaration of this interface supports
        /// only querying for status on a single-command. This is not a problem as almost
        /// all instances where you would implement IOleCommandTarget are for a single
        /// command. Implementations should ASSERT that cCmds is 1
        /// </summary>
        /// <param name="pguidCmdGroup"> Unique identifier of the command group; can be NULL to
        /// specify the standard group. All the commands that are passed in the prgCmds
        /// array must belong to the group specified by pguidCmdGroup</param>
        /// <param name="cCmds">The number of commands in the prgCmds array. For this
        /// interface declaration (which doesn't support arrays of OLECMD) this value
        /// MUST always be 1.</param>
        /// <param name="prgCmds">Reference to the command that is being queried for
        /// its status -- this parameter should be filled in with the appropriate
        /// values.</param>
        /// <param name="pCmdText">Pointer to an OLECMDTEXT structure in which to return
        /// name and/or status information of a single command. Can be NULL to indicate
        /// that the caller does not need this information. Note that because of
        /// marshalling issues w/ the OLECMDTEXT structure (can't figure out how to
        /// marshall it correctly) we required that this parameter be NULL (implementations
        /// should Assert on this). Note that IE currently passes NULL for this parameter
        /// for custom toolbar button implementations.</param>
        void QueryStatus(
            [In, MarshalAs(UnmanagedType.LPStruct)] Guid pguidCmdGroup,
            [In] uint cCmds,
            [In, Out] ref OLECMD prgCmds,
            [In, Out] IntPtr pCmdText);

        /// <summary>
        /// Executes a specified command or displays help for a command
        /// </summary>
        /// <param name="pguidCmdGroup">Pointer to unique identifier of the command group; can be
        /// NULL to specify the standard group</param>
        /// <param name="nCmdID">The command to be executed. This command must belong to the
        /// group specified with pguidCmdGroup</param>
        /// <param name="nCmdexecopt">Values taken from the OLECMDEXECOPT enumeration, which
        /// describe how the object should execute the command</param>
        /// <param name="pvaIn">Variant input parameter</param>
        /// <param name="pvaOut">Variant out parameter</param>
        void Exec(
            [In, MarshalAs(UnmanagedType.LPStruct)] Guid pguidCmdGroup,
            [In] uint nCmdID,
            [In] OLECMDEXECOPT nCmdexecopt,
            [In] ref object pvaIn,
            [In, Out] ref object pvaOut);
    }

    /// <summary>
    /// Generic COM/OLE command dispatching interface. This version of the declaration
    /// allows for the passing of NULL for the input parmaeter and object for the
    /// output parameter, thereby making it compatible with implementations that expect
    /// a NULL input parameter as an indicator that a command value request is occurring.
    /// </summary>
    [ComImport]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [Guid("b722bccb-4e68-101b-a2bc-00aa00404770")]
    public interface IOleCommandTargetGetCommandValue
    {
        /// <summary>
        /// Queries the object for the status of one or more commands generated by user
        /// interface events. Note that because we couldn't get COM interop to correctly
        /// marshall the array of OLECMDs, our declaration of this interface supports
        /// only querying for status on a single-command. This is not a problem as almost
        /// all instances where you would implement IOleCommandTarget are for a single
        /// command. Implementations should ASSERT that cCmds is 1
        /// </summary>
        /// <param name="pguidCmdGroup"> Unique identifier of the command group; can be NULL to
        /// specify the standard group. All the commands that are passed in the prgCmds
        /// array must belong to the group specified by pguidCmdGroup</param>
        /// <param name="cCmds">The number of commands in the prgCmds array. For this
        /// interface declaration (which doesn't support arrays of OLECMD) this value
        /// MUST always be 1.</param>
        /// <param name="prgCmds">Reference to the command that is being queried for
        /// its status -- this parameter should be filled in with the appropriate
        /// values.</param>
        /// <param name="pCmdText">Pointer to an OLECMDTEXT structure in which to return
        /// name and/or status information of a single command. Can be NULL to indicate
        /// that the caller does not need this information. Note that because of
        /// marshalling issues w/ the OLECMDTEXT structure (can't figure out how to
        /// marshall it correctly) we required that this parameter be NULL (implementations
        /// should Assert on this). Note that IE currently passes NULL for this parameter
        /// for custom toolbar button implementations.</param>
        void QueryStatus(
            [In, MarshalAs(UnmanagedType.LPStruct)] Guid pguidCmdGroup,
            [In] uint cCmds,
            [In, Out] ref OLECMD prgCmds,
            [In, Out] IntPtr pCmdText);

        /// <summary>
        /// Executes a specified command or displays help for a command
        /// </summary>
        /// <param name="pguidCmdGroup">Pointer to unique identifier of the command group; can be
        /// NULL to specify the standard group</param>
        /// <param name="nCmdID">The command to be executed. This command must belong to the
        /// group specified with pguidCmdGroup</param>
        /// <param name="nCmdexecopt">Values taken from the OLECMDEXECOPT enumeration, which
        /// describe how the object should execute the command</param>
        /// <param name="pvaIn">Variant input parameter</param>
        /// <param name="pvaOut">Variant out parameter</param>
        void Exec(
            [In, MarshalAs(UnmanagedType.LPStruct)] Guid pguidCmdGroup,
            [In] uint nCmdID,
            [In] OLECMDEXECOPT nCmdexecopt,
            [In] IntPtr pvaIn,
            [In, Out] ref object pvaOut);
    }

    /// <summary>
    /// Generic COM/OLE command dispatching interface. This version of the declaration
    /// allows for the passing of NULL for the input parmaeter and object for the
    /// output parameter, thereby making it compatible with implementations that expect
    /// a NULL input parameter as an indicator that a command value request is occurring.
    /// </summary>
    [ComImport]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [Guid("b722bccb-4e68-101b-a2bc-00aa00404770")]
    public interface IOleCommandTargetNullOutputParam
    {
        /// <summary>
        /// Queries the object for the status of one or more commands generated by user
        /// interface events. Note that because we couldn't get COM interop to correctly
        /// marshall the array of OLECMDs, our declaration of this interface supports
        /// only querying for status on a single-command. This is not a problem as almost
        /// all instances where you would implement IOleCommandTarget are for a single
        /// command. Implementations should ASSERT that cCmds is 1
        /// </summary>
        /// <param name="pguidCmdGroup"> Unique identifier of the command group; can be NULL to
        /// specify the standard group. All the commands that are passed in the prgCmds
        /// array must belong to the group specified by pguidCmdGroup</param>
        /// <param name="cCmds">The number of commands in the prgCmds array. For this
        /// interface declaration (which doesn't support arrays of OLECMD) this value
        /// MUST always be 1.</param>
        /// <param name="prgCmds">Reference to the command that is being queried for
        /// its status -- this parameter should be filled in with the appropriate
        /// values.</param>
        /// <param name="pCmdText">Pointer to an OLECMDTEXT structure in which to return
        /// name and/or status information of a single command. Can be NULL to indicate
        /// that the caller does not need this information. Note that because of
        /// marshalling issues w/ the OLECMDTEXT structure (can't figure out how to
        /// marshall it correctly) we required that this parameter be NULL (implementations
        /// should Assert on this). Note that IE currently passes NULL for this parameter
        /// for custom toolbar button implementations.</param>
        void QueryStatus(
            [In, MarshalAs(UnmanagedType.LPStruct)] Guid pguidCmdGroup,
            [In] uint cCmds,
            [In, Out] ref OLECMD prgCmds,
            [In, Out] IntPtr pCmdText);

        /// <summary>
        /// Executes a specified command or displays help for a command
        /// </summary>
        /// <param name="pguidCmdGroup">Pointer to unique identifier of the command group; can be
        /// NULL to specify the standard group</param>
        /// <param name="nCmdID">The command to be executed. This command must belong to the
        /// group specified with pguidCmdGroup</param>
        /// <param name="nCmdexecopt">Values taken from the OLECMDEXECOPT enumeration, which
        /// describe how the object should execute the command</param>
        /// <param name="pvaIn">Variant input parameter</param>
        /// <param name="pvaOut">Variant out parameter</param>
        void Exec(
            [In, MarshalAs(UnmanagedType.LPStruct)] Guid pguidCmdGroup,
            [In] uint nCmdID,
            [In] OLECMDEXECOPT nCmdexecopt,
            [In] ref object pvaIn,
            [In, Out] IntPtr pvaOut);
    }

    /// <summary>
    ///
    /// </summary>
    public enum OLECMDF : uint
    {
        None = 0,

        /// <summary>
        /// The command is supported by this object
        /// </summary>
        SUPPORTED = 1,

        /// <summary>
        /// The command is available and enabled
        /// </summary>
        ENABLED = 2,

        /// <summary>
        /// The command is an on-off toggle and is currently on
        /// </summary>
        LATCHED = 4,

        /// <summary>
        /// Reserved for future use
        /// </summary>
        NINCHED = 8
    }

    /// <summary>
    ///
    /// </summary>
    public enum OLECMDTEXTF : uint
    {
        /// <summary>
        /// No extra information is requested
        /// </summary>
        NONE = 0,

        /// <summary>
        /// The object should provide the localized name of the command
        /// </summary>
        NAME = 1,

        /// <summary>
        /// The object should provide a localized status string for the command
        /// </summary>
        STATUS = 2
    }

    /// <summary>
    ///
    /// </summary>
    public enum OLECMDEXECOPT : uint
    {
        /// <summary>
        /// Prompt the user for input or not, whichever is the default behavior
        /// </summary>
        DODEFAULT = 0,

        /// <summary>
        /// Execute the command after obtaining user input
        /// </summary>
        PROMPTUSER = 1,

        /// <summary>
        /// Execute the command without prompting the user. For example, clicking the Print
        /// toolbar button causes a document to be immediately printed without user input
        /// </summary>
        DONTPROMPTUSER = 2,

        /// <summary>
        /// Show help for the corresponding command, but do not execute
        /// </summary>
        SHOWHELP = 3
    }

    /// <summary>
    ///
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct OLECMD
    {
        /// <summary>
        /// A command identifier; taken from the OLECMDID enumeration
        /// </summary>
        public uint cmdID;

        /// <summary>
        /// Flags associated with cmdID; taken from the OLECMDF enumeration
        /// </summary>
        public OLECMDF cmdf;
    }

    public static class OLECMDID
    {
        // public const uint OPEN	= 1;
        // public const uint NEW	= 2;
        // public const uint SAVE	= 3;
        // public const uint SAVEAS	= 4;
        // public const uint SAVECOPYAS	= 5;
        // public const uint PRINT	= 6;
        // public const uint PRINTPREVIEW	= 7;
        // public const uint PAGESETUP	= 8;
        // public const uint SPELL	= 9;
        // public const uint PROPERTIES	= 10;
        // public const uint CUT	= 11;
        // public const uint COPY	= 12;
        // public const uint PASTE	= 13;
        // public const uint PASTESPECIAL	= 14;
        // public const uint UNDO	= 15;
        // public const uint REDO	= 16;
        // public const uint SELECTALL	= 17;
        // public const uint CLEARSELECTION	= 18;
        // public const uint ZOOM	= 19;
        // public const uint GETZOOMRANGE	= 20;
        // public const uint UPDATECOMMANDS	= 21;
        // public const uint REFRESH	= 22;
        // public const uint STOP	= 23;
        // public const uint HIDETOOLBARS	= 24;
        // public const uint SETPROGRESSMAX	= 25;
        // public const uint SETPROGRESSPOS	= 26;
        // public const uint SETPROGRESSTEXT	= 27;
        // public const uint SETTITLE	= 28;
        // public const uint SETDOWNLOADSTATE	= 29;
        // public const uint STOPDOWNLOAD	= 30;
        // public const uint ONTOOLBARACTIVATED	= 31;
        // public const uint FIND	= 32;
        // public const uint DELETE	= 33;
        // public const uint HTTPEQUIV	= 34;
        // public const uint HTTPEQUIV_DONE	= 35;
        // public const uint ENABLE_INTERACTION	= 36;
        // public const uint ONUNLOAD	= 37;
        // public const uint PROPERTYBAG2	= 38;
        // public const uint PREREFRESH	= 39;
        public const uint SHOWSCRIPTERROR = 40;
        public const uint SHOWMESSAGE = 41;
        // public const uint SHOWFIND	= 42;
        // public const uint SHOWPAGESETUP	= 43;
        // public const uint SHOWPRINT	= 44;
        // public const uint CLOSE	= 45;
        // public const uint ALLOWUILESSSAVEAS	= 46;
        // public const uint DONTDOWNLOADCSS	= 47;
        // public const uint UPDATEPAGESTATUS	= 48;
        // public const uint PRINT2	= 49;
        // public const uint PRINTPREVIEW2	= 50;
        // public const uint SETPRINTTEMPLATE	= 51;
        // public const uint GETPRINTTEMPLATE	= 52;
        // public const uint PAGEACTIONBLOCKED	= 55;
        // public const uint PAGEACTIONUIQUERY	= 56;
        // public const uint FOCUSVIEWCONTROLS	= 57;
        // public const uint FOCUSVIEWCONTROLSQUERY	= 58;
        // public const uint SHOWPAGEACTIONMENU	= 59;
    }

    /// <summary>
    /// Parameter pCmdtext to QueryStatus method but currently unused
    /// since IE passes NULL pointer. Note that if we want to start
    /// using this structure we will need to get the marshalling right
    /// (rgwz currently doesn't work as needed)
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct OLECMDTEXT
    {
        /// <summary>
        /// A value from the OLECMDTEXTF enumeration describing whether the rgwz parameter
        /// contains a command name or status text
        /// </summary>
        public OLECMDTEXTF cmdtextf;

        /// <summary>
        /// The number of characters actually written into the rgwz buffer before QueryStatus
        /// returns
        /// </summary>
        public uint cwActual;

        /// <summary>
        /// The number of elements in the rgwz arr
        /// </summary>
        public uint cwBuf;

        /// <summary>
        /// A caller-allocated array of wide characters to receive the command name or
        /// status text
        /// </summary>
        public char[] rgwz;
    }
}
