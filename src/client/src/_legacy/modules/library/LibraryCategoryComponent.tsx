import { Dispatch, SetStateAction, memo, useEffect, useState } from "react";
import { CollapseIcon, ExpandIcon } from "../../../assets/icons/chevron";
import { LibCategoryButton, LibCategoryHeader } from "./styled";
import { LibraryCategoryElement } from "./index";
import { LibraryCategory , ObjectType } from "../../models";

interface Props {
  category: LibraryCategory;
  selectedElement: string;
  setSelectedElement: Dispatch<SetStateAction<string>>;
  setSelectedElementType: Dispatch<SetStateAction<ObjectType>>;
  searchList?: LibraryCategory[];
}

/**
 * Component for a Category in the Library.
 * @param interface
 * @returns a drop-down menu of a given Category.
 */
const LibraryCategoryComponent = ({
  category,
  selectedElement,
  setSelectedElement,
  setSelectedElementType,
  searchList,
}: Props) => {
  const [expanded, setExpanded] = useState(false);
  const expandIcon = expanded ? ExpandIcon : CollapseIcon;

  useEffect(() => {
    if (searchList && searchList.length > 0 && searchList.includes(category)) {
      setExpanded(true);
    }
  }, [category, searchList]);

  return (
    <>
      <LibCategoryButton onClick={() => setExpanded(!expanded)}>
        <LibCategoryHeader>{category.name}</LibCategoryHeader>
        <img className="expandIcon" src={expandIcon} alt="expand-icon" />
      </LibCategoryButton>
      {expanded &&
        category?.nodes.map((item) => {
          return (
            <LibraryCategoryElement
              key={item.id}
              item={item}
              selectedElement={selectedElement}
              setSelectedElement={setSelectedElement}
              setSelectedElementType={setSelectedElementType}
            />
          );
        })}
    </>
  );
};

export default memo(LibraryCategoryComponent);
