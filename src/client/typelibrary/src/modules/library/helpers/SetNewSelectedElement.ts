import { LibItem } from "../../../models";

const SetNewSelectedElement = (item: LibItem, setSelectedElement: (selectedElementId: string) => void) => {
  setSelectedElement(item.id);
};

export default SetNewSelectedElement;
