import { ListType } from "../TypeEditorList";

const IsObjectAttributes = (listType: ListType) => {
  return listType === ListType.ObjectAttributes;
};

export default IsObjectAttributes;
