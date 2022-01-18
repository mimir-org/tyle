import { TypeEditorState } from "../../../redux/store/editor/types";
import { Dispatch } from "redux";
import { ListType } from "../TypeEditorList";
import { GetTypeEditorListDescriptor } from "./GetTypeEditorListDescriptor";
import { TypeEditorList } from "../index";
import { GetFlexForListType } from "./index";
import Validation from "../components/validation/Validation";

/**
 * Generates TypeEditor lists that are used for presenting and selecting options of a given type
 * @param state State of the TypeEditor at render
 * @param dispatch General dispatch function for the application
 */
export function GetTypeEditorLists(state: TypeEditorState, dispatch: Dispatch) {
  return Object.values(ListType).map((type, key) => {
    const listTypeEnum = ListType[type as keyof typeof ListType];
    const listDescriptor = GetTypeEditorListDescriptor(listTypeEnum, state, dispatch);

    return (
      listDescriptor?.isVisible && (
        <Validation
          key={key}
          flex={GetFlexForListType(listDescriptor.listType)}
          visible={listDescriptor.validation.visible}
          message={listDescriptor.validation.message}
        >
          <TypeEditorList
            items={listDescriptor.items}
            createLibraryType={listDescriptor.createLibraryType}
            listType={listDescriptor.listType}
            onPropertyChange={listDescriptor.onPropertyChange}
            onTerminalCategoryChange={listDescriptor?.onTerminalCategoryChange}
          />
        </Validation>
      )
    );
  });
}

export default GetTypeEditorLists;