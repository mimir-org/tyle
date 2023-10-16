import { DevTool } from "@hookform/devtools";
import { yupResolver } from "@hookform/resolvers/yup";
import { useServerValidation } from "common/hooks/server-validation/useServerValidation";
import { useNavigateOnCriteria } from "common/hooks/useNavigateOnCriteria";
import { Box, FormContainer } from "@mimirorg/component-library";
import { Loader } from "features/common/loader";
import { FormAttributes } from "features/entities/common/form-attributes/FormAttributes";
import { onSubmitForm } from "features/entities/common/utils/onSubmitForm";
import { usePrefilledForm } from "features/entities/common/utils/usePrefilledForm";
import { useSubmissionToast } from "features/entities/common/utils/useSubmissionToast";
import {
  TerminalFormFields,
  createDefaultTerminalFormFields,
  toTerminalFormFields,
  toTerminalTypeRequest,
  useTerminalMutation,
  useTerminalQuery,
} from "features/entities/terminal/TerminalForm.helpers";
import { TerminalFormBaseFields } from "features/entities/terminal/TerminalFormBaseFields";
import { terminalSchema } from "features/entities/terminal/terminalSchema";
import { FormProvider, useForm } from "react-hook-form";
import { useTranslation } from "react-i18next";
import { useTheme } from "styled-components";
import { FormMode } from "../types/formMode";
import { TerminalView } from "common/types/terminals/terminalView";
import { TerminalFormClassifiers } from "./TerminalFormClassifiers";

interface TerminalFormProps {
  defaultValues?: TerminalFormFields;
  mode?: FormMode;
}

export const TerminalForm = ({ defaultValues = createDefaultTerminalFormFields(), mode }: TerminalFormProps) => {
  const theme = useTheme();
  const { t } = useTranslation("entities");

  const formMethods = useForm<TerminalFormFields>({
    defaultValues: defaultValues,
    resolver: yupResolver(terminalSchema(t)),
  });

  const { handleSubmit, control, setError, reset } = formMethods;

  const query = useTerminalQuery();
  const mapper = (source: TerminalView) => toTerminalFormFields(source);
  const [_, isLoading] = usePrefilledForm(query, mapper, reset);

  const mutation = useTerminalMutation(query.data?.id, mode);
  useServerValidation(mutation.error, setError);
  useNavigateOnCriteria("/", mutation.isSuccess);

  const toast = useSubmissionToast(t("terminal.title"));

  const limited = false;

  return (
    <FormProvider {...formMethods}>
      <FormContainer
        onSubmit={handleSubmit((data) => onSubmitForm(toTerminalTypeRequest(data), mutation.mutateAsync, toast))}
      >
        {isLoading && <Loader />}
        {!isLoading && (
          <>
            <TerminalFormBaseFields limited={limited} mode={mode} />

            <Box display={"flex"} flex={3} flexDirection={"column"} gap={theme.mimirorg.spacing.multiple(6)}>
              <TerminalFormClassifiers />
            </Box>

            <Box display={"flex"} flex={3} flexDirection={"column"} gap={theme.mimirorg.spacing.multiple(6)}>
              <FormAttributes />
            </Box>
          </>
        )}
        <DevTool control={control} placement={"bottom-right"} />
      </FormContainer>
    </FormProvider>
  );
};
