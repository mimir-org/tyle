import { PlainLink } from "@mimirorg/component-library";
import { UseMutationResult } from "@tanstack/react-query";
import Button from "components/Button";
import { Table, Tbody, Td, Tr } from "components/Table";
import { onSubmitForm, useSubmissionToast } from "helpers/form.helpers";
import { useNavigateOnCriteria } from "hooks/useNavigateOnCriteria";
import { Aspect } from "types/common/aspect";
import { FormMode } from "types/formMode";
import { Direction } from "../../types/terminals/direction";
import { TerminalTypeRequest } from "../../types/terminals/terminalTypeRequest";
import { TerminalView } from "../../types/terminals/terminalView";
import { ReviewAndSubmitFormWrapper, SubmitButtonsWrapper } from "./ReviewAndSubmitForm.styled";
import { TerminalFormFields, toTerminalTypeRequest } from "./TerminalForm.helpers";

interface ReviewAndSubmitProps {
  terminalFormFields: TerminalFormFields;
  mutation: UseMutationResult<TerminalView, unknown, TerminalTypeRequest, unknown>;
  formRef: React.ForwardedRef<HTMLFormElement>;
  mode?: FormMode;
}

const ReviewAndSubmitForm = ({ terminalFormFields, mutation, formRef, mode }: ReviewAndSubmitProps) => {
  useNavigateOnCriteria("/", mutation.isSuccess);

  const toast = useSubmissionToast("terminal");

  const handleSubmit = (event: React.FormEvent) => {
    event.preventDefault();
    onSubmitForm(toTerminalTypeRequest(terminalFormFields), mutation.mutateAsync, toast);
  };

  return (
    <ReviewAndSubmitFormWrapper onSubmit={handleSubmit} ref={formRef}>
      <Table>
        <Tbody>
          <Tr>
            <Td>Name</Td>
            <Td>{terminalFormFields.name}</Td>
          </Tr>
          <Tr>
            <Td>Notation</Td>
            <Td>{terminalFormFields.notation}</Td>
          </Tr>
          <Tr>
            <Td>Aspect</Td>
            <Td>{terminalFormFields.aspect ? Aspect[terminalFormFields.aspect] : "undefined"}</Td>
          </Tr>
          <Tr>
            <Td>Purpose</Td>
            <Td>{terminalFormFields.purpose ? terminalFormFields.purpose.name : "undefined"}</Td>
          </Tr>
          <Tr>
            <Td>Description</Td>
            <Td>{terminalFormFields.description}</Td>
          </Tr>
          <Tr></Tr>
          <Tr>
            <Td>Medium</Td>
            <Td>{terminalFormFields.medium ? terminalFormFields.medium.name : "undefined"}</Td>
          </Tr>
          <Tr>
            <Td>Qualifier</Td>
            <Td>{Direction[terminalFormFields.qualifier]}</Td>
          </Tr>
          <Tr></Tr>
          <Tr>
            <Td>Classifiers</Td>
            <Td>
              {terminalFormFields.classifiers.length > 0
                ? terminalFormFields.classifiers.map((classifier) => classifier.name).join(", ")
                : "none"}
            </Td>
          </Tr>
          <Tr></Tr>
          <Tr>
            <Td>Attributes</Td>
            <Td>
              {terminalFormFields.attributes.length > 0
                ? terminalFormFields.attributes
                    .map(
                      (attribute) =>
                        `${attribute.attribute.name} (min: ${attribute.minCount}${
                          attribute.maxCount ? `, max: ${attribute.maxCount}` : ""
                        })`,
                    )
                    .join(", ")
                : "none"}
            </Td>
          </Tr>
        </Tbody>
      </Table>

      <SubmitButtonsWrapper>
        <Button type="submit">{mode === "edit" ? "Save changes" : "Create new type"}</Button>
        <PlainLink tabIndex={-1} to={"/"}>
          <Button tabIndex={0} as={"span"} variant={"outlined"}>
            Cancel
          </Button>
        </PlainLink>
      </SubmitButtonsWrapper>
    </ReviewAndSubmitFormWrapper>
  );
};

export default ReviewAndSubmitForm;
