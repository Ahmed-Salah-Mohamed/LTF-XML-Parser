# LTF-XML-Parser
XML Parser For LTF Schema

This project provides an LTF XML schema parser which parses a document path/stream into an LTF Document class with all of its properties & nested tags.

The LTF schema goes as follows:

LTF_Doc:
  -	Properties: id / tokenization / grammar / raw_text_char_length / raw_text_md5 / list of segments
  -	Each Segment:
    -	Properties: id / start_char / end_char / original_text / list of tokens
    - Each Token:
      - Properties: id / pos / morph / start_char / end_char / value (text inside token)
