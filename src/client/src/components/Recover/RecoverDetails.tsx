import { DevTool } from "@hookform/devtools";
import { yupResolver } from "@hookform/resolvers/yup";
import { Actionable, Button, Form, FormField, Input, Text } from "@mimirorg/component-library";
import { useGenerateChangePasswordSecret } from "api/user.queries";
import { AuthContent } from "components/AuthContent/AuthContent";
import { Error } from "components/Error/Error";
import { Processing } from "components/Processing/Processing";
import { recoverDetailsSchema } from "components/Recover/recoverDetailsSchema";
import { useExecuteOnCriteria } from "hooks/useExecuteOnCriteria";
import { useServerValidation } from "hooks/useServerValidation";
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
