import { Dispatch } from "redux";
import { CreateLibraryType } from "../../../models";
import { ResetRedux } from "../helpers";
import { updateCreateLibraryType } from "../../../redux/store/editor/editorSlice";

export const OnPropertyChange = <K extends keyof CreateLibraryType>(key: K, value: CreateLibraryType[K], dispatch: Dispatch) => {
  ResetRedux(dispatch, key, value);
  dispatch(updateCreateLibraryType({key, value}));
};
