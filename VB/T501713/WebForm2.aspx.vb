Imports DevExpress.Web
Imports DevExpress.Web.ASPxTreeList
Imports System
Imports System.Linq
Imports System.Web.UI
Imports System.Web.UI.WebControls

Namespace T501713

    Public Partial Class WebForm2
        Inherits Page

        Protected Sub Page_PreInit(ByVal sender As Object, ByVal e As EventArgs)
            AddHandler Master.PopupInit, AddressOf Master_PopupInit
            AddHandler Master.PopupCallback, AddressOf Master_PopupCallback
        End Sub

        Private Sub Master_PopupInit(ByVal sender As Object, ByVal e As EventArgs)
            If Session("isRendered") IsNot Nothing Then RenderTree(CType(sender, ASPxPopupControl))
        End Sub

        Private Sub Master_PopupCallback(ByVal source As Object, ByVal e As PopupWindowCallbackArgs)
            If Equals(e.Parameter, "tree") Then
                RenderTree(CType(source, ASPxPopupControl))
                Session("isRendered") = True
            End If
        End Sub

        Private Sub RenderTree(ByVal container As ASPxPopupControl)
            container.Controls.Clear()
            Dim tree As ASPxTreeList = New ASPxTreeList()
            tree.ID = "ASPxTreeList"
            container.Controls.Add(tree)
            tree.Width = Unit.Percentage(100)
            AddHandler tree.DataBinding, Sub(sender, e)
                TryCast(sender, ASPxTreeList).ParentFieldName = "Parent"
                TryCast(sender, ASPxTreeList).DataSource = Enumerable.Range(0, 10).[Select](Function(i) New With {.Id = i, .Name = "Name" & i, .Parent = i Mod 3}).ToList()
            End Sub
            tree.DataBind()
        End Sub
    End Class
End Namespace
