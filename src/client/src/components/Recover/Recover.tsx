import Completion from "components/Completion";
import MultiFactorAuthentication from "components/MultiFactorAuthentication";
import { useState } from "react";
import { useNavigate } from "react-router-dom";
import { QrCodeView } from "types/authentication/qrCodeView";
import { VerifyRequest } from "types/authentication/verifyRequest";
import RecoverDetails from "./RecoverDetails";
import RecoverPassword from "./RecoverPassword";
import RecoverVerification from "./RecoverVerification";

export type RecoverySteps = "DETAILS" | "VERIFY" | "PASSWORD" | "MFA" | "COMPLETE";

const Recover = () => {
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
            actionText: "Continue",
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
            actionText: "Next",
            onAction: () => setStage("PASSWORD"),
          }}
          cancel={{
            actionable: true,
            actionText: "Back",
            onAction: () => setStage("DETAILS"),
          }}
        />
      )}
      {stage === "PASSWORD" && (
        <RecoverPassword
          verificationInfo={verificationInfo}
          complete={{
            actionable: true,
            actionText: "Next",
            onAction: () => setStage("MFA"),
          }}
          cancel={{
            actionable: true,
            actionText: "Back",
            onAction: () => setStage("DETAILS"),
          }}
        />
      )}
      {stage === "MFA" && (
        <MultiFactorAuthentication
          title="Register authenticator"
          infoText="In order to use :Tyle, you must activate 2-factor authentication using an authenticator app. This is to ensure the quality fo the data created and stored in :Tyle."
          codeTitle="Scan the QR code"
          manualCodeTitle="Can't scan image?"
          manualCodeDescription="Copy this code into your authenticator app of choice"
          mfaInfo={mfaInfo}
          complete={{
            actionable: true,
            actionText: "Next",
            onAction: () => setStage("COMPLETE"),
          }}
          cancel={{
            actionable: true,
            actionText: "Back",
            onAction: () => setStage("DETAILS"),
          }}
        />
      )}
      {stage === "COMPLETE" && (
        <Completion
          title="Account recovery"
          infoText="Account recovery complete. Please return to the main page and log in."
          complete={{
            actionable: true,
            actionText: "Return",
            onAction: () => navigate("/"),
          }}
        />
      )}
    </>
  );
};

export default Recover;
