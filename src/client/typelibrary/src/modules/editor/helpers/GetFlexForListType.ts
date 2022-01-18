import { ListType } from "../TypeEditorList";

const GetFlexForListType = (listType: ListType) => {
  switch (listType) {
    case ListType.Terminals:
      return 2;
    case ListType.PredefinedAttributes:
      return 2;
    default:
      return 1;
  }
};

export default GetFlexForListType;
