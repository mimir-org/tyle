import { ListType } from "../TypeEditorList";

const RemoveBackground = (listType: ListType): boolean => {
  return !(listType === ListType.Terminals || listType === ListType.PredefinedAttributes);
};
export default RemoveBackground;
