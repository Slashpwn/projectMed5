using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

//namespace Mp3ID3v1
//{
    public class Mp3ID3
    {
        private static string[] _genres = { "Blues", "Classic Rock", "Country", "Dance", "Disco", "Funk", "Grunge", "Hip-Hop", "Jazz", "Metal", "New Age", "Oldies", "Other", "Pop", "R&B", "Rap", "Reggae", "Rock", "Techno", "Industrial", "Alternative", "Ska", "Death Metal", "Pranks", "Soundtrack", "Euro-Techno", "Ambient", "Trip-Hop", "Vocal", "Jazz+Funk", "Fusion", "Trance", "Classical", "Instrumental", "Acid", "House", "Game", "Sound Clip", "Gospel", "Noise", "AlternRock", "Bass", "Soul", "Punk", "Space", "Meditative", "Instrumental Pop", "Instrumental Rock", "Ethnic", "Gothic", "Darkwave", "Techno-Industrial", "Electronic", "Pop-Folk", "Eurodance", "Dream", "Southern Rock", "Comedy", "Cult", "Gangsta", "Top 40", "Christian Rap", "Pop/Funk", "Jungle", "Native American", "Cabaret", "New Wave", "Psychedelic", "Rave", "Showtunes", "Trailer", "Lo-Fi", "Tribal", "Acid Punk", "Acid Jazz", "Polka", "Retro", "Musical", "Rock & Roll", "Hard Rock", "Folk", "Folk-Rock", "National Folk", "Swing", "Fast Fusion", "Bebob", "Latin", "Revival", "Celtic", "Bluegrass", "Avantgarde", "Gothic Rock", "Progressive Rock", "Psychedelic Rock", "Symphonic Rock", "Slow Rock", "Big Band", "Chorus", "Easy Listening", "Acoustic", "Humour", "Speech", "Chanson", "Opera", "Chamber Music", "Sonata", "Symphony", "Booty Bass", "Primus", "Porn Groove", "Satire", "Slow Jam", "Club", "Tango", "Samba", "Folklore", "Ballad", "Power Ballad", "Rhythmic Soul", "Freestyle", "Duet", "Punk Rock", "Drum Solo", "A capella", "Euro-House", "Dance Hall" };
        private byte[] _id3Bytes = new byte[128];
        private string _mp3FilePath;

        public Mp3ID3(string MP3FilePath)
        {
            using (FileStream fs = File.OpenRead(MP3FilePath))
            {
                fs.Seek(-1* _id3Bytes.Length, SeekOrigin.End);
                fs.Read(_id3Bytes, 0, _id3Bytes.Length); 
                

            }
                _mp3FilePath = MP3FilePath;
        }

        private byte[] getByteRange(int start, int length)
        {
            List<byte> returnBytes = new List<byte>(); 
            for (int i = start; i < start + length; i++)
            {
                returnBytes.Add(_id3Bytes[i]); 
            }

            return returnBytes.ToArray(); 
        }

        public string Title
        {
            get { return Encoding.UTF8.GetString(getByteRange(3, 30)); }
        }

        public string Artist
        {
            get { return Encoding.UTF8.GetString(getByteRange(33, 30)); }

        }

        public string Album
        {
            get { return Encoding.UTF8.GetString(getByteRange(63, 30)); }

        }

        public int Year
        {
            get
            { 
                try { return int.Parse(Encoding.UTF8.GetString(getByteRange(93, 4))); }
                catch { return -1; }
            }

        }

        public String Comment
        {
            get { return Encoding.UTF8.GetString(getByteRange(97, 28)); }


        }

        public int TrackNumber
        {
            get 
            {
                try
                {
                    if ((int)_id3Bytes[125] != 0) return -1;
                    return (int)_id3Bytes[126];
                }catch { return -1; }
            } 
        }
        public string Genre
        {
            get
            {
                try
                {
                    return _genres[(int)_id3Bytes[127]];
                } catch { return ""; }
            }
        }



    }
//}
