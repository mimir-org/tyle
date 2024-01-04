import { DevTool } from "@hookform/devtools";
import { yupResolver } from "@hookform/resolvers/yup";
import { useChangePassword } from "api/user.queries";
import AuthContent from "components/AuthContent";
import Button from "components/Button";
import Error from "components/Error";
import Flexbox from "components/Flexbox";
import Form from "components/Form";
import FormField from "components/FormField";
import FormFieldset from "components/FormFieldset";
import Input from "components/Input";
import Processing from "components/Processing";
import Text from "components/Text";
import { useExecuteOnCriteria } from "hooks/useExecuteOnCriteria";
import { useServerValidation } from "hooks/useServerValidation";
import { useForm } from "react-hook-form";
import { useTheme } from "styled-components";
import { Actionable } from "types/actionable";
import { VerifyRequest } from "types/authentication/verifyRequest";
import { recoverPasswordSchema } from "./recoverPasswordSchema";

interface RecoverPasswordProps {
  verificationInfo: VerifyRequest;
  cancel?: Partial<Actionable>;
  complete?: Partial<Actionable>;
}

const RecoverPassword = ({ verificationInfo, cancel, complete }: RecoverPasswordProps) => {
  const theme = useTheme();

  const formMethods = useForm({
    resolver: yupResolver(recoverPasswordSchema()),
  });

  const { register, control, handleSubmit, setError, formState } = formMethods;
  const { errors } = formState;

  const mutation = useChangePassword();

  useServerValidation(mutation.error, setError);
  useExecuteOnCriteria(complete?.onAction, mutation.isSuccess);

  return (
    <AuthContent
      title="Choose a new password"
      firstRow={
        <>
          {mutation.isPending && <Processing>Processing</Processing>}
          {!mutation.isSuccess && !mutation.isPending && (
            <Form id={"password-form"} onSubmit={handleSubmit((data) => mutation.mutate(data))}>
              {mutation.isError && (
                <Error>
                  We were not able to change your password at this time. Please try again in about a minute. If the
                  issue persist,
                </Error>
              )}
              <FormFieldset>
                <Input type={"hidden"} value={verificationInfo.email} {...register("email")} />
                <Input type={"hidden"} value={verificationInfo.code} {...register("code")} />

                <FormField label="Password *" error={errors.password}>
                  <Input id="password" type="password" placeholder="**********" {...register("password")} />
                </FormField>

                <FormField label="Confirm password *" error={errors.confirmPassword}>
                  <Input
                    id="confirmPassword"
                    type="password"
                    placeholder="**********"
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
          <Text textAlign={"center"}>Enter the desired password for this account.</Text>
          <Flexbox gap={theme.tyle.spacing.xxl} alignSelf={"center"}>
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
