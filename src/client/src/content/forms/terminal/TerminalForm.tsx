import { DevTool } from "@hookform/devtools";
import { yupResolver } from "@hookform/resolvers/yup";
import { FormProvider, useFieldArray, useForm } from "react-hook-form";
import { useTranslation } from "react-i18next";
import { useTheme } from "styled-components";
import { Box } from "../../../complib/layouts";
import { useNavigateOnCriteria } from "../../../hooks/useNavigateOnCriteria";
import { useServerValidation } from "../../../hooks/useServerValidation";
import { Loader } from "../../common/loader";
import { FormAttributes } from "../common/form-attributes/FormAttributes";
import { onSubmitForm } from "../common/utils/onSubmitForm";
import { usePrefilledForm } from "../common/utils/usePrefilledForm";
import { useSubmissionToast } from "../common/utils/useSubmissionToast";
import { prepareAttributes, useTerminalMutation, useTerminalQuery } from "./TerminalForm.helpers";
import { TerminalFormContainer } from "./TerminalForm.styled";
import { TerminalFormBaseFields } from "./TerminalFormBaseFields";
import { terminalSchema } from "./terminalSchema";
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

  const formMethods = useForm<FormTerminalLib>({
    defaultValues: defaultValues,
    resolver: yupResolver(terminalSchema(t)),
  });

  const { register, handleSubmit, control, setError, reset } = formMethods;

  const attributeFields = useFieldArray({ control, name: "attributeIdList" });

  const query = useTerminalQuery();
  const [_, isLoading] = usePrefilledForm(query, mapTerminalLibCmToFormTerminalLib, reset);

  const mutation = useTerminalMutation(isEdit);
  useServerValidation(mutation.error, setError);
  useNavigateOnCriteria("/", mutation.isSuccess);

  const toast = useSubmissionToast(t("terminal.title"));

  return (
    <FormProvider {...formMethods}>
      <TerminalFormContainer
        onSubmit={handleSubmit((data) => onSubmitForm(mapFormTerminalLibToApiModel(data), mutation.mutateAsync, toast))}
      >
        {isLoading && <Loader />}
        {!isLoading && (
          <>
            <TerminalFormBaseFields />

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
    </FormProvider>
  );
};
