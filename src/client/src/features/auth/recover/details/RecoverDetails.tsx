import { DevTool } from "@hookform/devtools";
import { yupResolver } from "@hookform/resolvers/yup";
import { useServerValidation } from "common/hooks/server-validation/useServerValidation";
import { useExecuteOnCriteria } from "common/hooks/useExecuteOnCriteria";
import { Actionable, Button, Form, FormField, Input, Text } from "@mimirorg/component-library";
import { useGenerateChangePasswordSecret } from "external/sources/user/user.queries";
import { AuthContent } from "features/auth/common/auth-content/AuthContent";
import { Error } from "features/auth/common/error/Error";
import { Processing } from "features/auth/common/processing/Processing";
import { recoverDetailsSchema } from "features/auth/recover/details/recoverDetailsSchema";
import { useForm } from "react-hook-form";
import { useTranslation } from "react-i18next";

interface RecoverDetailsProps {
  setUserEmail: (email: string) => void;
  complete?: Partial<Actionable>;
}

export const RecoverDetails = ({ complete, setUserEmail }: RecoverDetailsProps) => {
  const { t } = useTranslation("auth");

  const formMethods = useForm({
    resolver: yupResolver(recoverDetailsSchema(t)),
  });

  const { register, control, handleSubmit, setError, formState } = formMethods;
  const { errors } = formState;

  const mutation = useGenerateChangePasswordSecret();

  useServerValidation(mutation.error, setError);
  useExecuteOnCriteria(complete?.onAction, mutation.isSuccess);

  return (
    <AuthContent
      title={t("recover.details.title")}
      firstRow={
        <>
          {mutation.isLoading && <Processing>{t("recover.processing")}</Processing>}
          {!mutation.isSuccess && !mutation.isLoading && (
            <Form
              id={"details-form"}
              onSubmit={handleSubmit((data) => {
                setUserEmail(data.email);
                mutation.mutate(data.email);
              })}
            >
              {mutation.isError && <Error>{t("recover.details.error")}</Error>}
              <FormField label={`${t("recover.details.email")} *`} error={errors.email}>
                <Input
                  id="email"
                  type="email"
                  placeholder={t("recover.details.placeholders.email")}
                  {...register("email")}
                />
              </FormField>
            </Form>
          )}
          <DevTool control={control} placement={"bottom-right"} />
        </>
      }
      secondRow={
        <>
          <Text textAlign={"center"}>{t("recover.details.info.text")}</Text>
          <Button type={"submit"} form={"details-form"} alignSelf={"center"}>
            {complete?.actionText}
          </Button>
        </>
      }
    />
  );
};
