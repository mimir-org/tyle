import { DevTool } from "@hookform/devtools";
import { useFieldArray, useForm } from "react-hook-form";
import { useTranslation } from "react-i18next";
import { useTheme } from "styled-components";
import { Box } from "../../../complib/layouts";
import { useCreateTerminal, useUpdateTerminal } from "../../../data/queries/tyle/queriesTerminal";
import { useNavigateOnCriteria } from "../../../hooks/useNavigateOnCriteria";
import { Loader } from "../../common/Loader";
import { FormAttributes } from "../common/FormAttributes";
import { onSubmitForm } from "../common/onSubmitForm";
import { usePrefilledForm } from "../common/usePrefilledForm";
import { useSubmissionToast } from "../common/useSubmissionToast";
import { prepareAttributes, useTerminalQuery } from "./TerminalForm.helpers";
import { TerminalFormContainer } from "./TerminalForm.styled";
import { TerminalFormBaseFields } from "./TerminalFormBaseFields";
import {
  createEmptyFormTerminalLib,
  FormTerminalLib,
  mapFormTerminalLibToApiModel,
  mapTerminalLibCmToFormTerminalLib,
} from "./types/formTerminalLib";

interface TerminalFormProps {
  defaultValues?: FormTerminalLib;
  isEdit?: boolean;
}

export const TerminalForm = ({ defaultValues = createEmptyFormTerminalLib(), isEdit }: TerminalFormProps) => {
  const theme = useTheme();
  const { t } = useTranslation();
  const { register, handleSubmit, control, reset } = useForm<FormTerminalLib>({ defaultValues });
  const attributeFields = useFieldArray({ control, name: "attributeIdList" });

  const query = useTerminalQuery();
  const [_, isLoading] = usePrefilledForm(query, mapTerminalLibCmToFormTerminalLib, reset);

  const createMutation = useCreateTerminal();
  const updateMutation = useUpdateTerminal();
  const targetMutation = isEdit ? updateMutation : createMutation;

  const toast = useSubmissionToast(t("terminal.title"));

  useNavigateOnCriteria("/", targetMutation.isSuccess);

  return (
    <TerminalFormContainer
      onSubmit={handleSubmit((data) =>
        onSubmitForm(mapFormTerminalLibToApiModel(data), targetMutation.mutateAsync, toast)
      )}
    >
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
              preprocess={prepareAttributes}
            />
          </Box>
        </>
      )}
      <DevTool control={control} placement={"bottom-right"} />
    </TerminalFormContainer>
  );
};
