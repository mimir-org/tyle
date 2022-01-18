import { ListType } from "../TypeEditorList";

const IsSimpleTypes = (listType: ListType) => {
  return listType === ListType.SimpleTypes;
};

export default IsSimpleTypes;
