using Autodesk.Revit.DB;
using Autodesk.Revit.UI.Selection;
using System.Collections.Generic;

public class ElementSelFilter : ISelectionFilter
{
    private string _category = null;
    private List<string> _categories = null;

    public ElementSelFilter(string category)
    {
        _category = category;
    }
    public ElementSelFilter(List<string> list)
    {
        _categories = list;
    }
    public bool AllowElement(Element elem)
    {
        if (elem?.Category?.Name != null)
        {
            if (_category != null)
            {
                if (elem.Category.Name == _category)
                    return true;
            }
            else
            {
                if (_categories.Contains(elem.Category.Name))
                    return true;
            }
        }
        return false;
    }

    public bool AllowReference(Reference reference, XYZ position)
    {
        return false;
    }
}
