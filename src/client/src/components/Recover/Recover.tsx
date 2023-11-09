import Completion from "components/Completion";
import MultiFactorAuthentication from "components/MultiFactorAuthentication";
import { useState } from "react";
import { useTranslation } from "react-i18next";
import { useNavigate } from "react-router-dom";
import { QrCodeView } from "types/authentication/qrCodeView";
import { VerifyRequest } from "types/authentication/verifyRequest";
import RecoverDetails from "./RecoverDetails";
import RecoverPassword from "./RecoverPassword";
import RecoverVerification from "./RecoverVerification";

export type RecoverySteps = "DETAILS" | "VERIFY" | "PASSWORD" | "MFA" | "COMPLETE";

const Recover = () => {
  const { t } = useTranslation("auth");
  const [stage, setStage] = useState<RecoverySteps>("DETAILS");
  const [email, setEmail] = useState("");
  const [mfaInfo, setMfaInfo] = useState<QrCodeView>({ code: "", manualCode: "" });
  const [verificationInfo, setVerificationInfo] = useState<VerifyRequest>({ email: "", code: "" });
  const navigate = useNavigate();

  return (
    <>
      {stage === "DETAILS" && (
        <RecoverDetails
          setUserEmail={setEmail}
          complete={{
            actionable: true,
            actionText: t("recover.details.submit"),
            onAction: () => setStage("VERIFY"),
          }}
        />
      )}
      {stage === "VERIFY" && (
        <RecoverVerification
          email={email}
          setVerificationInfo={setVerificationInfo}
          setMfaInfo={setMfaInfo}
          complete={{
            actionable: true,
            actionText: t("recover.next"),
            onAction: () => setStage("PASSWORD"),
          }}
          cancel={{
            actionable: true,
            actionText: t("recover.back"),
            onAction: () => setStage("DETAILS"),
          }}
        />
      )}
      {stage === "PASSWORD" && (
        <RecoverPassword
          verificationInfo={verificationInfo}
          complete={{
            actionable: true,
            actionText: t("recover.next"),
            onAction: () => setStage("MFA"),
          }}
          cancel={{
            actionable: true,
            actionText: t("recover.back"),
            onAction: () => setStage("DETAILS"),
          }}
        />
      )}
      {stage === "MFA" && (
        <MultiFactorAuthentication
          title={t("recover.mfa.title")}
          infoText={t("recover.mfa.info.text")}
          codeTitle={t("recover.mfa.code.title")}
          manualCodeTitle={t("recover.mfa.manual.title")}
          manualCodeDescription={t("recover.mfa.manual.description")}
          mfaInfo={mfaInfo}
          complete={{
            actionable: true,
            actionText: t("recover.next"),
            onAction: () => setStage("COMPLETE"),
          }}
          cancel={{
            actionable: true,
            actionText: t("recover.back"),
            onAction: () => setStage("DETAILS"),
          }}
        />
      )}
      {stage === "COMPLETE" && (
        <Completion
          title={t("recover.complete.title")}
          infoText={t("recover.complete.info.text")}
          complete={{
            actionable: true,
            actionText: t("recover.complete.return"),
            onAction: () => navigate("/"),
          }}
        />
      )}
    </>
  );
};

export default Recover;
