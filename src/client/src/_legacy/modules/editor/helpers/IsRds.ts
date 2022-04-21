import { ListType } from "../TypeEditorList";

const IsRds = (listType: ListType) => {
  return listType === ListType.Rds;
};

export default IsRds;
