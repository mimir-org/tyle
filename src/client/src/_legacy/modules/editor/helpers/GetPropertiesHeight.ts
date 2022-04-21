import { Size } from "../../../../compLibrary/size";

/**
 * Calculate and return height of Choose Properties lists in Type Editor.
 * @param isFull Whether to use full size of Properties list, or shrunk. Inverse of whether Inspector is open.
 * @returns Height of Properties list
 */
export const GetPropertiesHeight = (isFull: boolean): number => {
  return (
    document.body.clientHeight + (isFull ? Size.TypeEditorPropertiesFull_BASELINE : Size.TypeEditorPropertiesShrunk_BASELINE)
  );
};

export default GetPropertiesHeight;