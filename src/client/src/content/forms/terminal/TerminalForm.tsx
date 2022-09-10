import { DevTool } from "@hookform/devtools";
import { useFieldArray, useForm } from "react-hook-form";
import { useTranslation } from "react-i18next";
import { useTheme } from "styled-components";
import { Box } from "../../../complib/layouts";
import { useNavigateOnCriteria } from "../../../hooks/useNavigateOnCriteria";
import { Loader } from "../../common/loader";
import { FormAttributes } from "../common/form-attributes/FormAttributes";
import { onSubmitForm } from "../common/utils/onSubmitForm";
import { usePrefilledForm } from "../common/utils/usePrefilledForm";
import { useSubmissionToast } from "../common/utils/useSubmissionToast";
import { prepareAttributes, useTerminalMutation, useTerminalQuery } from "./TerminalForm.helpers";
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

  const toast = useSubmissionToast(t("terminal.title"));

  const mutation = useTerminalMutation(isEdit);
  useNavigateOnCriteria("/", mutation.isSuccess);

  return (
    <TerminalFormContainer
      onSubmit={handleSubmit((data) => onSubmitForm(mapFormTerminalLibToApiModel(data), mutation.mutateAsync, toast))}
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
