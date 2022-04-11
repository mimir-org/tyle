import { memo } from "react";
import { setValidation } from "../../redux/store/validation/validationSlice";
import { Modal } from "../../../compLibrary/modal/Modal";
import { Button } from "../../../compLibrary/buttons";
import { TextResources } from "../../../assets/text";
import { NotificationModalContent } from "../../../compLibrary/modal/variants/alert/NotificationModalContent";
import {
  useAppDispatch,
  useAppSelector,
  validationSelector,
} from "../../redux/store";

/**
 * Component to handle validation in Mimir. If an action requires a feedback a box with information will appear.
 * @returns a box with a message and a close button.
 */
const ValidationModule = () => {
  const dispatch = useAppDispatch();
  const validation = useAppSelector(validationSelector);

  const onClick = () => {
    dispatch(setValidation({ valid: true, message: "" }));
  };

  return (
    <Modal isOpen={!validation.valid} onExit={onClick}>
      <NotificationModalContent isWarning description={validation.message}>
        <Button onClick={onClick} text={TextResources.Validation_Ok} />
      </NotificationModalContent>
    </Modal>
  );
};

export default memo(ValidationModule);
