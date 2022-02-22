import { ListType } from "../TypeEditorList";

const IsLocationAttributes = (listType: ListType) => {
  return listType === ListType.LocationAttributes;
};

export default IsLocationAttributes;
