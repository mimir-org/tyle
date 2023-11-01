import { DevTool } from "@hookform/devtools";
import { yupResolver } from "@hookform/resolvers/yup";
import { useServerValidation } from "hooks/server-validation/useServerValidation";
import { useNavigateOnCriteria } from "hooks/useNavigateOnCriteria";
import { Box, FormContainer } from "@mimirorg/component-library";
import { Loader } from "components/Loader";
import { FormAttributes } from "components/FormAttributes/FormAttributes";
import { onSubmitForm, usePrefilledForm, useSubmissionToast } from "helpers/form.helpers";
import {
  TerminalFormFields,
  createDefaultTerminalFormFields,
  toTerminalFormFields,
  toTerminalTypeRequest,
  useTerminalMutation,
  useTerminalQuery,
} from "components/TerminalForm/TerminalForm.helpers";
import { TerminalFormBaseFields } from "components/TerminalForm/TerminalFormBaseFields";
import { terminalSchema } from "components/TerminalForm/terminalSchema";
import { FormProvider, useForm } from "react-hook-form";
import { useTranslation } from "react-i18next";
import { useTheme } from "styled-components";
import { FormMode } from "../../common/types/formMode";
import { TerminalView } from "common/types/terminals/terminalView";
import { FormClassifiers } from "./TerminalFormClassifiers";

interface TerminalFormProps {
  defaultValues?: TerminalFormFields;
  mode?: FormMode;
}

const TerminalForm = ({ defaultValues = createDefaultTerminalFormFields(), mode }: TerminalFormProps) => {
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
              <FormClassifiers />
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

export default TerminalForm;