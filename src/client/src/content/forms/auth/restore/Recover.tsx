import { MimirorgQrCodeCm, MimirorgVerifyAm } from "@mimirorg/typelibrary-types";
import { useState } from "react";
import { useTranslation } from "react-i18next";
import { useNavigate } from "react-router-dom";
import { Completion } from "../common/Completion";
import { MultiFactorAuthentication } from "../common/MultiFactorAuthentication";
import { RecoverDetails } from "./components/RecoverDetails";
import { RecoverPassword } from "./components/RecoverPassword";
import { RecoverVerification } from "./components/RecoverVerification";

export type RecoverySteps = "DETAILS" | "VERIFY" | "PASSWORD" | "MFA" | "COMPLETE";

export const Recover = () => {
  const { t } = useTranslation();
  const [stage, setStage] = useState<RecoverySteps>("DETAILS");
  const [email, setEmail] = useState("");
  const [mfaInfo, setMfaInfo] = useState<MimirorgQrCodeCm>({ code: "", manualCode: "" });
  const [verificationInfo, setVerificationInfo] = useState<MimirorgVerifyAm>({ email: "", code: "" });
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
            actionText: t("common.next"),
            onAction: () => setStage("PASSWORD"),
          }}
          cancel={{
            actionable: true,
            actionText: t("common.back"),
            onAction: () => setStage("DETAILS"),
          }}
        />
      )}
      {stage === "PASSWORD" && (
        <RecoverPassword
          verificationInfo={verificationInfo}
          complete={{
            actionable: true,
            actionText: t("common.next"),
            onAction: () => setStage("MFA"),
          }}
          cancel={{
            actionable: true,
            actionText: t("common.back"),
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
            actionText: t("common.next"),
            onAction: () => setStage("COMPLETE"),
          }}
          cancel={{
            actionable: true,
            actionText: t("common.back"),
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
            actionText: t("common.return"),
            onAction: () => navigate("/"),
          }}
        />
      )}
    </>
  );
};
