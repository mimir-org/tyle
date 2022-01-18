import { Dispatch } from "redux";
import { changeTypeEditorValidationVisibility, changeTypeEditorVisibility } from "../../../redux/store/editor/editorSlice";

const onCloseEditor = (dispatch: Dispatch) => {
  dispatch(changeTypeEditorValidationVisibility(false));
  dispatch(changeTypeEditorVisibility(false));
};

export default onCloseEditor;
