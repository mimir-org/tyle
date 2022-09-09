import { DevTool } from "@hookform/devtools";
import { useFieldArray, useForm } from "react-hook-form";
import { useTheme } from "styled-components";
import { Box } from "../../../complib/layouts";
import { useCreateTerminal, useUpdateTerminal } from "../../../data/queries/tyle/queriesTerminal";
import { useNavigateOnCriteria } from "../../../hooks/useNavigateOnCriteria";
import { Loader } from "../../common/Loader";
import { FormAttributes } from "../common/FormAttributes";
import { prepareAttributes, usePrefilledTerminalData, useTerminalSubmissionToast } from "./TerminalForm.helpers";
import { TerminalFormContainer } from "./TerminalForm.styled";
import { TerminalFormBaseFields } from "./TerminalFormBaseFields";
import { createEmptyFormTerminalLib, FormTerminalLib, mapFormTerminalLibToApiModel } from "./types/formTerminalLib";

interface TerminalFormProps {
  defaultValues?: FormTerminalLib;
  isEdit?: boolean;
}

export const TerminalForm = ({ defaultValues = createEmptyFormTerminalLib(), isEdit }: TerminalFormProps) => {
  const theme = useTheme();
  const { register, handleSubmit, control, reset } = useForm<FormTerminalLib>({ defaultValues });
  const attributeFields = useFieldArray({ control, name: "attributeIdList" });

  const [_, isLoading] = usePrefilledTerminalData(reset);

  const createMutation = useCreateTerminal();
  const updateMutation = useUpdateTerminal();
  const targetMutation = isEdit ? updateMutation : createMutation;

  const toastNodeSubmission = useTerminalSubmissionToast();
  const onSubmit = (data: FormTerminalLib) => {
    const submittable = mapFormTerminalLibToApiModel(data);
    const submissionPromise = targetMutation.mutateAsync(submittable);
    toastNodeSubmission(submissionPromise);
    return submissionPromise;
  };

  useNavigateOnCriteria("/", targetMutation.isSuccess);

  return (
    <TerminalFormContainer onSubmit={handleSubmit((data) => onSubmit(data))}>
      {isLoading && <Loader />}
      {!isLoading && (
        <>
          <TerminalFormBaseFields control={control} register={register} />

          <Box display={"flex"} flex={3} flexDirection={"column"} gap={theme.tyle.spacing.multiple(6)}>
            <FormAttributes
              register={(index) => register(`attributeIdList.${index}`)}
              fields={attributeFields.fields}
              append={attributeFields.append}
              remove={attributeFields.remove}
              prepareAttributes={prepareAttributes}
            />
          </Box>
        </>
      )}
      <DevTool control={control} placement={"bottom-right"} />
    </TerminalFormContainer>
  );
};
