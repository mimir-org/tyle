import { createAppSelector, combineAppSelectors } from "./hooks";

export const isLibraryStateFetchingSelector = createAppSelector(
  (state) => state.library.fetching,
  (fetching) => fetching
);

export const isUserStateFetchingSelector = createAppSelector(
  (state) => state.userState.fetching,
  (fetching) => fetching
);

export const isEditorFetchingSelector = createAppSelector(
  (state) => state.editor.fetching,
  (fetching) => fetching
);

export const isFetchingSelector = combineAppSelectors(
  [
    isLibraryStateFetchingSelector,
    isUserStateFetchingSelector,
    isEditorFetchingSelector,
  ],
  (isLibraryStateFetching, isUserStateFetching, isEditorFetchingSelector) =>
    isLibraryStateFetching || isUserStateFetching || isEditorFetchingSelector
);

export const userStateSelector = createAppSelector(
  (state) => state.userState,
  (userState) => userState
);

export const usernameSelector = createAppSelector(
  (state) => state.userState.user?.name,
  (username) => username
);

export const userErrorsSelector = createAppSelector(
    (state) => state.userState.apiError,
    (errors) => errors
);

export const editorStateSelector = createAppSelector(
  (state) => state.editor,
  (editor) => editor
);

export const editorErrorsSelector = createAppSelector(
    (state) => state.editor.apiError,
    (errors) => errors
);

export const librarySelector = createAppSelector(
  (state) => state.library,
  (library) => library
);

export const libraryErrorsSelector = createAppSelector(
    (state) => state.library.apiError,
    (errors) => errors
);

export const validationSelector = createAppSelector(
  (state) => state.validation,
  (validation) => validation
);