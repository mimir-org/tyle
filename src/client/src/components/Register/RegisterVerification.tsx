import { DevTool } from "@hookform/devtools";
import { useGenerateMfa, useVerification } from "api/user.queries";
import AuthContent from "components/AuthContent";
import Button from "components/Button";
import Digits from "components/Digits";
import Flexbox from "components/Flexbox";
import FormErrorBanner from "components/FormErrorBanner";
import Input from "components/Input";
import MotionVerifyForm from "components/MotionVerifyForm";
import Processing from "components/Processing";
import Text from "components/Text";
import { useExecuteOnCriteria } from "hooks/useExecuteOnCriteria";
import { Controller, useForm } from "react-hook-form";
import { useTheme } from "styled-components";
import { Actionable } from "types/actionable";
import { QrCodeView } from "types/authentication/qrCodeView";
import { VerifyRequest } from "types/authentication/verifyRequest";
import { onSubmitForm } from "./RegisterVerification.helpers";

type VerificationProps = Pick<VerifyRequest, "email"> & {
  setMfaInfo: (info: QrCodeView) => void;
  cancel?: Partial<Actionable>;
  complete?: Partial<Actionable>;
};

const RegisterVerification = ({ email, setMfaInfo, cancel, complete }: VerificationProps) => {
  const theme = useTheme();
  const { control, register, handleSubmit } = useForm<VerifyRequest>();

  const generateMfaMutation = useGenerateMfa();
  const verificationMutation = useVerification();

  const showError = generateMfaMutation.isError || verificationMutation.isError;
  const showProcessing = verificationMutation.isPending || generateMfaMutation.isPending;
  const showInput = !verificationMutation.isSuccess && !verificationMutation.isPending;

  useExecuteOnCriteria(complete?.onAction, verificationMutation.isSuccess && generateMfaMutation.isSuccess);

  return (
    <AuthContent
      title="Verify e-mail"
      firstRow={
        <>
          {showProcessing && <Processing>Processing</Processing>}
          {showError && (
            <FormErrorBanner>
              We were not able verify your ownership of this email. Verify that that you have entered the correct code
              and please try again in about a minute. If the issue persist, contact support.
            </FormErrorBanner>
          )}
          {showInput && (
            <MotionVerifyForm
              id={"verify-form"}
              onSubmit={handleSubmit((data) =>
                onSubmitForm(data, verificationMutation.mutateAsync, generateMfaMutation.mutateAsync, setMfaInfo),
              )}
              layout
            >
              <Input type={"hidden"} value={email} {...register("email")} />
              <Controller
                control={control}
                name={"code"}
                render={({ field: { value, onChange } }) => <Digits value={value} onChange={onChange} />}
              />
            </MotionVerifyForm>
          )}
          <DevTool control={control} placement={"bottom-right"} />
        </>
      }
      secondRow={
        <>
          <Text textAlign={"center"}>
            We have sent a six-digit code to your e-mail. Please enter the code within 1 hour, or the code will expire
            and you will have to restart the registration. Make sure to check your spam/junk folder for the e-mail.
          </Text>
          <Flexbox gap={theme.tyle.spacing.xxl} alignSelf={"center"}>
            {cancel?.actionable && (
              <Button variant={"outlined"} onClick={cancel.onAction}>
                {cancel.actionText}
              </Button>
            )}
            {complete?.actionable && (
              <Button type={"submit"} form={"verify-form"}>
                {complete.actionText}
              </Button>
            )}
          </Flexbox>
        </>
      }
    />
  );
};

export default RegisterVerification;
