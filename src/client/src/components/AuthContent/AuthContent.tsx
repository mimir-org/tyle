import { ReactNode } from "react";
import { AuthContentContainer, AuthContentSection } from "./AuthContent.styled";
import AuthContentHeader, { AuthContentHeaderProps } from "./AuthContentHeader";

export type AuthContentProps = AuthContentHeaderProps & {
  firstRow: ReactNode;
  secondRow?: ReactNode;
};

/**
 * Component that facilitates common functionality that are often used in unauthenticated views
 *
 * @param props
 * @constructor
 */
const AuthContent = (props: AuthContentProps) => {
  const { title, subtitle, firstRow, secondRow } = props;
  const showHeader = title || subtitle;

  return (
    <AuthContentContainer>
      {showHeader && <AuthContentHeader title={title} subtitle={subtitle} />}
      <AuthContentSection>{firstRow}</AuthContentSection>
      <AuthContentSection>{secondRow}</AuthContentSection>
    </AuthContentContainer>
  );
};

export default AuthContent;
