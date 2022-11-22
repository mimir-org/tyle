import { AuthContentContainer, AuthContentSection } from "features/auth/common/auth-content/AuthContent.styled";
import { AuthContentHeader, AuthContentHeaderProps } from "features/auth/common/auth-content/AuthContentHeader";
import { ReactNode } from "react";

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
export const AuthContent = (props: AuthContentProps) => {
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
