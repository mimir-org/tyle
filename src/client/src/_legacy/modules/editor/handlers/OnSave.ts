import { Dispatch } from "redux";
import { OnCloseEditor } from "./index";
import { CreateLibraryType } from "../../../models";
import { changeTypeEditorValidationVisibility, saveLibraryType } from "../../../redux/store/editor/editorSlice";
import { IsTypeEditorSubmissionValid } from "../validators";

const OnSave = (dispatch: Dispatch, createLibraryType: CreateLibraryType) => {
  if (IsTypeEditorSubmissionValid(createLibraryType)) {
    dispatch(changeTypeEditorValidationVisibility(false));
    dispatch(saveLibraryType(createLibraryType));
    OnCloseEditor(dispatch);
  } else {
    dispatch(changeTypeEditorValidationVisibility(true));
  }
};

export default OnSave;
