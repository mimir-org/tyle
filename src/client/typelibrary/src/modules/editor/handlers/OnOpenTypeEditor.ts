import { Dispatch } from "redux";
import { ObjectType } from "../../../models";
import { GetLibraryType } from "../helpers";
import { changeTypeEditorVisibility, fetchCreateLibraryType } from "../../../redux/store/editor/editorSlice";

export const OnOpenTypeEditor = (
  selectedElement: string,
  selectedElementType: ObjectType,
  onChange: () => void,
  dispatch: Dispatch
) => {
  if (selectedElement && selectedElementType !== ObjectType.NotSet) {
    const filter = GetLibraryType(selectedElementType);
    dispatch(fetchCreateLibraryType({selectedType: selectedElement, filter}));
    onChange();
  } else {
    dispatch(changeTypeEditorVisibility(true));
    onChange();
  }
};
