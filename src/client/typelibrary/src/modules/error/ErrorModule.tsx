import { Dispatch } from "redux";
import { useEffect, useState } from "react";
import { ErrorBody, ErrorBox, ErrorHeaderBox, ErrorItem } from "./styled";
import { CloseIcon } from "../../assets/icons/close";
import { TextResources } from "../../assets/text";
import { BadRequestData } from "../../models/webclient";
import { deleteTypeEditorError } from "../../redux/store/editor/editorSlice";
import { editorStateSelector, useAppSelector } from "../../redux/store";

interface ErrorMessage {
  key: string;
  module: string;
  message?: string;
  errorData?: BadRequestData;
}

interface Props {
  dispatch: Dispatch;
}

/**
 * Module to handle errors coming from the server.
 * @param interface
 * @returns a box with the error messages.
 */
const ErrorModule = ({ dispatch }: Props) => {
  const [visible, setVisible] = useState(false);
  const [errors, setErrors] = useState<ErrorMessage[]>([]);
  const typeEditorState = useAppSelector(editorStateSelector);

  const closeHeader = () => {
    if (errors) {
      errors.forEach((error) => {
        if (error.key) {
          dispatch(deleteTypeEditorError(error.key));
        }
      });
    }
    setVisible(false);
  };

  useEffect(() => {
    const errorList: ErrorMessage[] = [];

    if (typeEditorState.apiError) {
      typeEditorState.apiError.forEach((error) => {
        if (error)
          errorList.push({
            module: "TypeEditor",
            key: error.key,
            message: error.errorMessage,
            errorData: error.errorData,
          });
      });
    }

    setErrors(errorList);
    setVisible(errorList.length > 0);
  }, [typeEditorState.apiError]);

  return (
    <>
      <ErrorBox visible={visible}>
        <ErrorHeaderBox>
          <img src={CloseIcon} alt="Close error message" onClick={() => closeHeader()} className="icon" />
          {TextResources.Error_Tile}
        </ErrorHeaderBox>
        <ErrorBody>
          {errors?.map((x, index) => {
            return (
              <ErrorItem key={x.module + index}>
                <h3>{x.module}</h3>
                <p>{x.message}</p>
                {x.errorData?.items?.map((y) => {
                  return (
                    <p key={y.key}>
                      {y.key}: {JSON.stringify(y.value)}
                    </p>
                  );
                })}
              </ErrorItem>
            );
          })}
        </ErrorBody>
      </ErrorBox>
    </>
  );
};
export default ErrorModule;
