import { DevTool } from "@hookform/devtools";
import { yupResolver } from "@hookform/resolvers/yup";
import { Actionable, Button, Flexbox, Form, FormField, FormFieldset, Input, Text } from "@mimirorg/component-library";
import { useChangePassword } from "api/user.queries";
import AuthContent from "components/AuthContent";
import Error from "components/Error";
import Processing from "components/Processing";
import { useExecuteOnCriteria } from "hooks/useExecuteOnCriteria";
import { useServerValidation } from "hooks/useServerValidation";
import { useForm } from "react-hook-form";
import { useTranslation } from "react-i18next";
import { useTheme } from "styled-components";
import { VerifyRequest } from "types/authentication/verifyRequest";
import { recoverPasswordSchema } from "./recoverPasswordSchema";

interface RecoverPasswordProps {
  verificationInfo: VerifyRequest;
  cancel?: Partial<Actionable>;
  complete?: Partial<Actionable>;
}

const RecoverPassword = ({ verificationInfo, cancel, complete }: RecoverPasswordProps) => {
  const theme = useTheme();
  const { t } = useTranslation("auth");

  const formMethods = useForm({
    resolver: yupResolver(recoverPasswordSchema(t)),
  });

  const { register, control, handleSubmit, setError, formState } = formMethods;
  const { errors } = formState;

  const mutation = useChangePassword();

  useServerValidation(mutation.error, setError);
  useExecuteOnCriteria(complete?.onAction, mutation.isSuccess);

  return (
    <AuthContent
      title={t("recover.password.title")}
      firstRow={
        <>
          {mutation.isLoading && <Processing>{t("recover.processing")}</Processing>}
          {!mutation.isSuccess && !mutation.isLoading && (
            <Form id={"password-form"} onSubmit={handleSubmit((data) => mutation.mutate(data))}>
              {mutation.isError && <Error>{t("recover.password.error")}</Error>}
              <FormFieldset>
                <Input type={"hidden"} value={verificationInfo.email} {...register("email")} />
                <Input type={"hidden"} value={verificationInfo.code} {...register("code")} />

                <FormField label={`${t("recover.password.password")} *`} error={errors.password}>
                  <Input
                    id="password"
                    type="password"
                    placeholder={t("recover.password.placeholders.password")}
                    {...register("password")}
                  />
                </FormField>

                <FormField label={`${t("recover.password.confirmPassword")} *`} error={errors.confirmPassword}>
                  <Input
                    id="confirmPassword"
                    type="password"
                    placeholder={t("recover.password.placeholders.password")}
                    {...register("confirmPassword")}
                  />
                </FormField>
              </FormFieldset>
            </Form>
          )}
          <DevTool control={control} placement={"bottom-right"} />
        </>
      }
      secondRow={
        <>
          <Text textAlign={"center"}>{t("recover.password.info.text")}</Text>
          <Flexbox gap={theme.mimirorg.spacing.xxl} alignSelf={"center"}>
            {cancel?.actionable && (
              <Button variant={"outlined"} onClick={cancel.onAction}>
                {cancel.actionText}
              </Button>
            )}
            {complete?.actionable && (
              <Button type={"submit"} form={"password-form"}>
                {complete.actionText}
              </Button>
            )}
          </Flexbox>
        </>
      }
    />
  );
};

export default RecoverPassword;
