/* eslint-disable @typescript-eslint/no-explicit-any */
import { IsInterface, IsLocation, IsObjectBlock, IsProduct, IsTransport } from "./index";
import { CreateLibraryType } from "../../../models";
import { clearAllTerminalTypes, updateCreateLibraryType } from "../../../redux/store/editor/editorSlice";

const ResetRedux = (dispatch: any, key: keyof CreateLibraryType, value: any) => {
  if (key === "aspect" && IsLocation(value)) {
    dispatch(updateCreateLibraryType({ key: "terminalTypes", value: [] }));
    dispatch(updateCreateLibraryType({ key: "attributeTypes", value: [] }));
  }
  if (key === "aspect" && !IsLocation(value)) {
    dispatch(updateCreateLibraryType({ key: "locationType", value: "" }));
    dispatch(updateCreateLibraryType({ key: "predefinedAttributes", value: [] }));
    dispatch(updateCreateLibraryType({ key: "attributeTypes", value: [] }));
  }
  if (key === "aspect" && !IsProduct(value)) {
    dispatch(updateCreateLibraryType({ key: "simpleTypes", value: [] }));
  }
  if (key === "objectType" && IsObjectBlock(value)) {
    dispatch(updateCreateLibraryType({ key: "terminalTypeId", value: "" }));
  }
  if (key === "objectType" && (IsTransport(value) || IsInterface(value))) {
    dispatch(updateCreateLibraryType({ key: "terminalTypes", value: [] }));
  }
  if (key === "terminalTypeId") {
    dispatch(clearAllTerminalTypes());
  }
};

export default ResetRedux;
