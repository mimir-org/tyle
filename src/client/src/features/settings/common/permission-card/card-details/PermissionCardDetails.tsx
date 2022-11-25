import { Dd, Dl, Dt } from "features/settings/common/permission-card/card-details/PermissionCardDetails.styled";
import { Fragment } from "react";

interface PermissionCardDetailsProps {
  descriptors: { [key: string]: string };
}

export const PermissionCardDetails = ({ descriptors }: PermissionCardDetailsProps) => (
  <Dl>
    {descriptors &&
      Object.keys(descriptors).map((k, i) => (
        <Fragment key={i}>
          <Dt>{k}</Dt>
          <Dd>{descriptors[k]}</Dd>
        </Fragment>
      ))}
  </Dl>
);
