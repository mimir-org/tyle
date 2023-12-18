import Logo from "components/Logo";
import PlainLink from "components/PlainLink";

const HeaderHomeLink = () => {
  return (
    <PlainLink to={"/"} height={"100%"}>
      <Logo height={"100%"} width={"100%"} alt="Link to start page" />
    </PlainLink>
  );
};

export default HeaderHomeLink;
