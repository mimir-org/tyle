import { ListType } from "../TypeEditorList";

const IsPredefinedAttributes = (listType: ListType) => {
  return listType === ListType.PredefinedAttributes;
};

export default IsPredefinedAttributes;
