import { ObjectType } from "../../../models";

const SetNewSelectedElementType = (libraryType: ObjectType, setSelectedElementType: (elementType: ObjectType) => void) =>
  setSelectedElementType(libraryType);

export default SetNewSelectedElementType;
