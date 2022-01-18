import { ChangeEvent, memo, useMemo, useState } from "react";
import { TextResources } from "../../assets/text";
import { SearchInput } from "../../compLibrary/input/text";
import { LibraryCategoryComponent } from "./index";
import { librarySelector, useAppSelector } from "../../redux/store";
import { LibBody } from "./styled";
import { TypeEditorModule } from "../editor";
import { GetFilteredLibCategories, GetLibCategories } from "./helpers";
import { LibraryCategory, ObjectType } from "../../models";

interface Props {
  search: (text: string) => void;
  searchString: string;
}

const LibraryComponent = ({ search, searchString }: Props) => {
  const [selectedElement, setSelectedElement] = useState("");
  const [selectedElementType, setSelectedElementType] = useState<ObjectType>(null);
  const libState = useAppSelector(librarySelector);

  const libCategories = useMemo(() => GetLibCategories(libState), [libState]);
  const filteredCategories = useMemo(() => GetFilteredLibCategories(libCategories, searchString), [libCategories, searchString]);

  const filterCatBySearch = (): LibraryCategory[] => {
    return searchString ? filteredCategories : libCategories;
  };

  const onChange = (e: ChangeEvent<HTMLInputElement>) => search(e.target.value);

  const typeEditorOpen = () => {
    setSelectedElement("");
    setSelectedElementType(null);
  };

  return (
    <>
      <SearchInput placeholder={TextResources.Library_SearchBox_Placeholder} onChange={onChange} />
      <TypeEditorModule selectedElement={selectedElement} selectedElementType={selectedElementType} onChange={typeEditorOpen} />
      <LibBody>
        {filterCatBySearch().map((category) => {
          return (
            <LibraryCategoryComponent
              selectedElement={selectedElement}
              setSelectedElement={setSelectedElement}
              setSelectedElementType={setSelectedElementType}
              key={category.name}
              category={category}
              searchList={filteredCategories}
            />
          );
        })}
      </LibBody>
    </>
  );
};

export default memo(LibraryComponent);
