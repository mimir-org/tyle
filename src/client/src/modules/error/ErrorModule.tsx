import ExitButton from "../../compLibrary/modal/components/ExitButton";
import { useEffect, useState } from "react";
import { ErrorBody, ErrorItem, ErrorItemText, ErrorItemTitle } from "./ErrorModule.styled";
import { TextResources } from "../../assets/text";
import { BadRequestData } from "../../models/webclient";
import { deleteLibraryError } from "../../redux/store/library/librarySlice";
import { deleteUserError } from "../../redux/store/user/userSlice";
import { InfoModalContent } from "../../compLibrary/modal/variants/info/InfoModalContent";
import { Modal } from "../../compLibrary/modal/Modal";
import { Color } from "../../compLibrary/colors";
import { deleteTypeEditorError } from "../../redux/store/editor/editorSlice";
import {
  editorErrorsSelector,
  libraryErrorsSelector,
  userErrorsSelector,
  useAppDispatch,
  useAppSelector,
} from "../../redux/store";

interface ErrorMessage {
  key: string;
  module: string;
  message: string;
  errorData: BadRequestData;
}

/**
 * Module to handle errors coming from the server.
 * @returns a box with the error messages.
 */
const ErrorModule = () => {
  const dispatch = useAppDispatch();
  const [visible, setVisible] = useState(false);
  const [errors, setErrors] = useState<ErrorMessage[]>([]);
  const userErrors = useAppSelector(userErrorsSelector);
  const libraryErrors = useAppSelector(libraryErrorsSelector);
  const editorErrors = useAppSelector(editorErrorsSelector);

  const closeHeader = () => {
    if (errors) {
      errors.forEach((error) => {
        if (error.key) {
          dispatch(deleteUserError(error.key));
          dispatch(deleteLibraryError(error.key));
          dispatch(deleteTypeEditorError(error.key));
        }
      });
    }
    setVisible(false);
  };

  useEffect(() => {
    const errorList: ErrorMessage[] = [];

    if (userErrors) {
      userErrors.forEach((error) => {
        if (error)
          errorList.push({
            module: "User",
            key: error.key,
            message: error.errorMessage,
            errorData: error.errorData,
          });
      });
    }

    if (libraryErrors) {
      libraryErrors.forEach((error) => {
        if (error)
          errorList.push({
            module: "Library",
            key: error.key,
            message: error.errorMessage,
            errorData: error.errorData,
          });
      });
    }

    if (editorErrors) {
      editorErrors.forEach((error) => {
        if (error)
          errorList.push({
            module: "Editor",
            key: error.key,
            message: error.errorMessage,
            errorData: error.errorData,
          });
      });
    }

    setErrors(errorList);
    setVisible(errorList.length > 0);
  }, [userErrors, libraryErrors, editorErrors]);

  return (
    <Modal isBlurred isOpen={visible} onExit={closeHeader}>
      <InfoModalContent title={TextResources.Error_Tile} color={Color.RedWarning}>
        <ExitButton onClick={closeHeader} />
        <ErrorBody>
          {errors?.map((x, index) => {
            return (
              <ErrorItem key={x.module + index}>
                <ErrorItemTitle>{x.module}</ErrorItemTitle>
                <ErrorItemText>{x.message}</ErrorItemText>
                {x.errorData?.items?.map((y) => {
                  return (
                    <ErrorItemText key={y.key}>
                      {y.key}: {JSON.stringify(y.value)}
                    </ErrorItemText>
                  );
                })}
              </ErrorItem>
            );
          })}
        </ErrorBody>
      </InfoModalContent>
    </Modal>
  );
};
export default ErrorModule;
