import { Dispatch, SetStateAction } from "react";
import { GetAspectColor, GetObjectIcon } from "../../helpers";
import { AspectColorType, LibItem, ObjectType } from "../../models";
import { SetNewSelectedElement, SetNewSelectedElementType } from "./helpers";
import { LibElement, LibElementIcon, LibElementText } from "./styled";

interface Props {
  item: LibItem;
  selectedElement: string;
  setSelectedElement: Dispatch<SetStateAction<string>>;
  setSelectedElementType: Dispatch<SetStateAction<ObjectType>>;
}

/**
 * Component for an element in a LibraryCategory drop-down menu.
 * @param interface
 * @returns a draggable element.
 */
const LibraryCategoryElement = ({
  item,
  selectedElement,
  setSelectedElement,
  setSelectedElementType,
}: Props) => {
  return (
    <LibElement
      active={selectedElement === item.id}
      onClick={() => {
        SetNewSelectedElement(item, setSelectedElement);
        SetNewSelectedElementType(item.libraryType, setSelectedElementType);
      }}
      key={item.id}
    >
      <LibElementText>{item.name}</LibElementText>
      <LibElementIcon color={GetAspectColor(item, AspectColorType.Main, false)}>
        {(item.libraryType === ObjectType.Interface || item.libraryType === ObjectType.Transport) && (
          <img src={GetObjectIcon(item)} alt="aspect color" className="icon" draggable="false" />
        )}
      </LibElementIcon>
    </LibElement>
  );
};

export default LibraryCategoryElement;
