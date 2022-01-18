import { memo } from "react";
import { Dispatch } from "redux";
import { NotificationComponent } from "../../compLibrary/box";
import { useAppSelector, validationSelector } from "../../redux/store";
import { setValidation } from "../../redux/store/validation/validationSlice";

interface Props {
  dispatch: Dispatch;
}

/**
 * Component to handle validation. If an action requires a feedback a box with information will appear.
 * @param interface
 * @returns a box with a message and a close button.
 */
const ValidationModule = ({ dispatch }: Props) => {
  const validation = useAppSelector(validationSelector);

  const onClick = () => {
    dispatch(setValidation({ valid: true, message: "" }));
  };

  return !validation.valid && <NotificationComponent onClick={onClick} message={validation.message} warning />;
};

export default memo(ValidationModule);
