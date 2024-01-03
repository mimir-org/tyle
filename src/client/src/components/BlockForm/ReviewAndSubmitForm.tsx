import { Button, PlainLink } from "@mimirorg/component-library";
import { UseMutationResult } from "@tanstack/react-query";
import { Table, Tbody, Td, Tr } from "components/Table";
import { onSubmitForm, useSubmissionToast } from "helpers/form.helpers";
import { useNavigateOnCriteria } from "hooks/useNavigateOnCriteria";
import { BlockTypeRequest } from "types/blocks/blockTypeRequest";
import { BlockView } from "types/blocks/blockView";
import { Aspect } from "types/common/aspect";
import { FormMode } from "types/formMode";
import { Direction } from "types/terminals/direction";
import { BlockFormFields, toBlockTypeRequest } from "./BlockForm.helpers";
import { ReviewAndSubmitFormWrapper, SubmitButtonsWrapper } from "./ReviewAndSubmitForm.styled";

interface ReviewAndSubmitProps {
  blockFormFields: BlockFormFields;
  mutation: UseMutationResult<BlockView, unknown, BlockTypeRequest, unknown>;
  formRef: React.ForwardedRef<HTMLFormElement>;
  mode?: FormMode;
}

const ReviewAndSubmitForm = ({ blockFormFields, mutation, formRef, mode }: ReviewAndSubmitProps) => {
  useNavigateOnCriteria("/", mutation.isSuccess);

  const toast = useSubmissionToast("block");

  const handleSubmit = (event: React.FormEvent) => {
    event.preventDefault();
    onSubmitForm(toBlockTypeRequest(blockFormFields), mutation.mutateAsync, toast);
  };

  return (
    <ReviewAndSubmitFormWrapper onSubmit={handleSubmit} ref={formRef}>
      <Table>
        <Tbody>
          <Tr>
            <Td>Name</Td>
            <Td>{blockFormFields.name}</Td>
          </Tr>
          <Tr>
            <Td>Notation</Td>
            <Td>{blockFormFields.notation}</Td>
          </Tr>
          <Tr>
            <Td>Aspect</Td>
            <Td>{blockFormFields.aspect ? Aspect[blockFormFields.aspect] : "undefined"}</Td>
          </Tr>
          <Tr>
            <Td>Purpose</Td>
            <Td>{blockFormFields.purpose ? blockFormFields.purpose.name : "undefined"}</Td>
          </Tr>
          <Tr>
            <Td>Description</Td>
            <Td>{blockFormFields.description}</Td>
          </Tr>
          <Tr>
            <Td>Classifiers</Td>
            <Td>
              {blockFormFields.classifiers.length > 0
                ? blockFormFields.classifiers.map((classifier) => classifier.name).join(", ")
                : "none"}
            </Td>
          </Tr>
          <Tr>
            <Td>Attributes</Td>
            <Td>
              {blockFormFields.attributes.length > 0
                ? blockFormFields.attributes
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
          <Tr>
            <Td>Terminals</Td>
            <Td>
              {blockFormFields.terminals.length > 0
                ? blockFormFields.terminals
                    .map(
                      (terminal) =>
                        `${terminal.terminalName} (${Direction[terminal.direction]}, min: ${terminal.minCount}${
                          terminal.maxCount ? `, max: ${terminal.maxCount}` : ""
                        })`,
                    )
                    .join(", ")
                : "none"}
            </Td>
          </Tr>
          <Tr>
            <Td>Symbol</Td>
            <Td>{blockFormFields.symbol ? blockFormFields.symbol.label : "none"}</Td>
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
