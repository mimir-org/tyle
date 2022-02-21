import { ListType } from "../TypeEditorList";

const SwitchBackground = (listType: ListType): boolean => {
  return listType === ListType.Rds || listType === ListType.ObjectAttributes;
};
export default SwitchBackground;
