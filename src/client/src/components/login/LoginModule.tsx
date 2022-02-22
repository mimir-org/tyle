import { useState } from "react";
import { LibraryIcon } from "../../assets/icons/modules";
import { TextResources } from "../../assets/text";
import { Button } from "../../compLibrary/buttons";
import { Icon } from "../../compLibrary/icon";
import { Input } from "../../compLibrary/input/text";
import {
  LoginWrapper,
  LoginContent,
  LoginHeader,
  LoginInputLabel,
  LoginButtonsWrapper,
} from "./LoginModule.styled";

export const LoginModule = () => {
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");

  return (
    <LoginWrapper>
      <LoginContent>
        <Icon size={100} src={LibraryIcon} alt="library-icon" />
        <LoginHeader>{TextResources.Login_Label}</LoginHeader>
        <LoginInputLabel htmlFor="email">
          {TextResources.Login_Label_Email}
        </LoginInputLabel>
        <Input
          type="text"
          name="email"
          value={email}
          onChange={(e) => setEmail(e.target.value)}
          autoFocus
          required
        ></Input>
        <LoginInputLabel htmlFor="password">
          {TextResources.Login_Label_Password}
        </LoginInputLabel>
        <Input
          type="password"
          name="password"
          value={password}
          onChange={(e) => setPassword(e.target.value)}
          required
        ></Input>
        <LoginButtonsWrapper>
          <Button
            onClick={() => null}
            text={TextResources.Login_Label_Register}
          />
          <Button onClick={() => null} text={TextResources.Login_Label} />
        </LoginButtonsWrapper>
      </LoginContent>
    </LoginWrapper>
  );
};
