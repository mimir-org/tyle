import { ListType } from "../TypeEditorList";

const RemoveHover = (listType: ListType): boolean => {
  return listType === ListType.Rds || listType === ListType.PredefinedAttributes;
};
export default RemoveHover;
