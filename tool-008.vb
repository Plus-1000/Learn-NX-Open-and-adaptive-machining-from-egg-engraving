' NX 12.0.0.27
' Journal created by wangjian on Wed Aug 23 11:06:28 2023 Malay Peninsula Standard Time
'
Imports System
Imports NXOpen

Module NXJournal
Sub Main (ByVal args() As String) 

Dim theSession As NXOpen.Session = NXOpen.Session.GetSession()
Dim workPart As NXOpen.Part = theSession.Parts.Work

Dim displayPart As NXOpen.Part = theSession.Parts.Display

' ----------------------------------------------
'   Menu: Application->Manufacturing->Manufacturing
' ----------------------------------------------
Dim markId1 As NXOpen.Session.UndoMarkId = Nothing
markId1 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Visible, "Enter Manufacturing")

theSession.ApplicationSwitchImmediate("UG_APP_MANUFACTURING")

Dim kinematicConfigurator1 As NXOpen.SIM.KinematicConfigurator = Nothing
kinematicConfigurator1 = workPart.CreateKinematicConfigurator()

' ----------------------------------------------
'   Menu: Insert->Tool...
' ----------------------------------------------
Dim markId2 As NXOpen.Session.UndoMarkId = Nothing
markId2 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Visible, "Create Tool")

Dim nCGroup1 As NXOpen.CAM.NCGroup = CType(workPart.CAMSetup.CAMGroupCollection.FindObject("GENERIC_MACHINE"), NXOpen.CAM.NCGroup)

Dim nCGroup2 As NXOpen.CAM.NCGroup = Nothing
nCGroup2 = workPart.CAMSetup.CAMGroupCollection.CreateTool(nCGroup1, "mill_planar", "MILL", NXOpen.CAM.NCGroupCollection.UseDefaultName.False, "MILL009")

Dim markId3 As NXOpen.Session.UndoMarkId = Nothing
markId3 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, "Start")

Dim tool1 As NXOpen.CAM.Tool = CType(nCGroup2, NXOpen.CAM.Tool)

Dim millToolBuilder1 As NXOpen.CAM.MillToolBuilder = Nothing
millToolBuilder1 = workPart.CAMSetup.CAMGroupCollection.CreateMillToolBuilder(tool1)

theSession.SetUndoMarkName(markId3, "Milling Tool-5 Parameters Dialog")

' ----------------------------------------------
'   Dialog Begin Milling Tool-5 Parameters
' ----------------------------------------------
Dim markId4 As NXOpen.Session.UndoMarkId = Nothing
markId4 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, "Milling Tool-5 Parameters")

millToolBuilder1.TlNumberBuilder.Value = 9

theSession.DeleteUndoMark(markId4, Nothing)

Dim markId5 As NXOpen.Session.UndoMarkId = Nothing
markId5 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, "Milling Tool-5 Parameters")

Dim nXObject1 As NXOpen.NXObject = Nothing
nXObject1 = millToolBuilder1.Commit()

theSession.DeleteUndoMark(markId5, Nothing)

theSession.SetUndoMarkName(markId3, "Milling Tool-5 Parameters")

millToolBuilder1.Destroy()

theSession.DeleteUndoMark(markId3, Nothing)

' ----------------------------------------------
'   Menu: Tools->Journal->Stop Recording
' ----------------------------------------------

End Sub
End Module