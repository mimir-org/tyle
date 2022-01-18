import { ListType } from "../TypeEditorList";

const IsTerminal = (listType: ListType) => {
  return listType === ListType.Terminals;
};

export default IsTerminal;
